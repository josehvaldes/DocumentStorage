using DocuStorage.Common;
using DocuStorate.Data.Model;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuStorate.Data.Services
{
    public class GroupDataService : IGroupDataService
    {
        public void AssignToUser(int userId, int[] groups)
        {
            var query = "select * from AssignGroupsToUser(@userid, @groups)";
            using var con = new NpgsqlConnection(Configuration.DatabaseConnection());
            con.Open();

            using var cmd = new NpgsqlCommand(query, con);
            cmd.Parameters.AddWithValue("userid", userId);
            cmd.Parameters.Add("groups", NpgsqlTypes.NpgsqlDbType.Array | NpgsqlTypes.NpgsqlDbType.Integer).Value = groups;
            var val = cmd.ExecuteNonQuery();
        }

        public Group Create(Group group)
        {
            var query = "insert into Groups(name) values (@name) RETURNING id";

            using var con = new NpgsqlConnection(Configuration.DatabaseConnection());
            con.Open();

            using var cmd = new NpgsqlCommand(query, con);
            cmd.Parameters.AddWithValue("name", group.Name);

            var val = cmd.ExecuteScalar();
            group.Id = Int32.Parse(val?.ToString() ?? "0");

            return group;
        }

        public List<Group> GetAll()
        {
            List<Group> result = new List<Group>();
            var query = "select * from GetGroups()";

            using var con = new NpgsqlConnection(Configuration.DatabaseConnection());
            con.Open();
            using var cmd = new NpgsqlCommand(query, con);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                var group = new Group()
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                };

                result.Add(group);
            }

            return result;
        }

        public List<Group> GetByUser(int userId)
        {
            List<Group> result = new List<Group>();
            var query = "select * from GetGroupsbyUser(@userid)";

            using var con = new NpgsqlConnection(Configuration.DatabaseConnection());
            con.Open();
            using var cmd = new NpgsqlCommand(query, con);
            cmd.Parameters.AddWithValue("userid", userId);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                var group = new Group()
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                };

                result.Add(group);
            }

            return result;
        }

        public void Delete(int id)
        {
            using var con = new NpgsqlConnection(Configuration.DatabaseConnection());
            con.Open();
            var query = "delete from groups where id = @id";
            using var cmd = new NpgsqlCommand(query, con);
            cmd.Parameters.AddWithValue("id", id);
            var val = cmd.ExecuteNonQuery();
        }
    }
}

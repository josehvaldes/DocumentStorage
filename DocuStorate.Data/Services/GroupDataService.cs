namespace DocuStorage.Data.Services;
using DocuStorage.Common;
using DocuStorage.Common.Data.Model;
using DocuStorage.Common.Data.Services;
using Npgsql;



public class GroupDataService : IGroupDataService
{
    public void AssignToUser(int userId, int[] groups)
    {
        var query = "select * from assign_groups_to_user(@userId, @groups)";
        using var con = new NpgsqlConnection(Configuration.DatabaseConnection());
        con.Open();

        using var cmd = new NpgsqlCommand(query, con);
        cmd.Parameters.AddWithValue("userId", userId);
        cmd.Parameters.Add("groups", NpgsqlTypes.NpgsqlDbType.Array | NpgsqlTypes.NpgsqlDbType.Integer).Value = groups;
        var val = cmd.ExecuteNonQuery();
    }

    public Group Create(Group group)
    {
        var query = "insert into groups(name) values (@name) RETURNING id";

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
        var query = "select * from get_groups()";

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
        var query = "select * from get_groups_by_user(@userId)";

        using var con = new NpgsqlConnection(Configuration.DatabaseConnection());
        con.Open();
        using var cmd = new NpgsqlCommand(query, con);
        cmd.Parameters.AddWithValue("userId", userId);
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


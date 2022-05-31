namespace DocuStorage.Data.Services;

using Npgsql;
using DocuStorage.Common;
using DocuStorage.Common.Data.Model;
using DocuStorage.Common.Data.Services;
using Microsoft.Extensions.Configuration;

public class UserDataService : IUserDataService
{
    private readonly IConfiguration _configuration;

    public UserDataService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public User Create(User user) 
    {
        using var con = new NpgsqlConnection(_configuration.DatabaseConnection());
        con.Open();

        var query = "insert into users(username, password, role) values (@username, @passwd, @role) RETURNING id";

        using var cmd = new NpgsqlCommand(query, con);
        cmd.Parameters.AddWithValue("username", user.Username);
        cmd.Parameters.AddWithValue("passwd", user.Password);
        cmd.Parameters.AddWithValue("role", user.Role);

        var val = cmd.ExecuteScalar();
        user.Id = Int32.Parse(val?.ToString()??"0");

        return user;
    }

    public void Delete(int userId)
    {
        using var con = new NpgsqlConnection(_configuration.DatabaseConnection());
        con.Open();
        var query = "delete from users where id = @userid";
        using var cmd = new NpgsqlCommand(query, con);
        cmd.Parameters.AddWithValue("userid", userId);
        var val = cmd.ExecuteNonQuery();
    }

    public User? Get(int id) 
    {
        using var con = new NpgsqlConnection(_configuration.DatabaseConnection());
        con.Open();

        var query = "select * from get_user(@user_id)";

        using var cmd = new NpgsqlCommand(query, con);
        cmd.Parameters.AddWithValue("user_id", id);

        using var reader = cmd.ExecuteReader();
        User? user = null;
        while (reader.Read())
        {
            user = new User()
            {
                Id = reader.GetInt32(0),
                Username = reader.GetString(1),
                Password = reader.GetString(2),
                Role = reader.GetInt32(3)
            };
        }
            
        return user;
    }

    public User? Get(User userparams)
    {
        using var con = new NpgsqlConnection(_configuration.DatabaseConnection());
        con.Open();

        var query = "select * from get_user(@username, @passwd)";

        using var cmd = new NpgsqlCommand(query, con);
        cmd.Parameters.AddWithValue("username", userparams.Username);
        cmd.Parameters.AddWithValue("passwd", userparams.Password);

        using var reader = cmd.ExecuteReader();
        User? user = null;
        while (reader.Read())
        {
            user = new User()
            {
                Id = reader.GetInt32(0),
                Username = reader.GetString(1),
                Password = reader.GetString(2),
                Role = reader.GetInt32(3),
            };
        }

        return user;
    }

    public List<User> GetAll() 
    {
        List<User> users = new List<User>();

        var cs = _configuration.DatabaseConnection();

        using (var con = new NpgsqlConnection(cs)) 
        {
            con.Open();

            var query = "select * from get_all_users()";

            using var cmd = new NpgsqlCommand(query, con);

            using var reader = cmd.ExecuteReader();

            while (reader.Read()) 
            {
                users.Add(new User() { 
                    Id = reader.GetInt32(0), 
                    Username = reader.GetString(1), 
                    Password = reader.GetString(2),
                    Role = reader.GetInt32(3),
                });
            }
        }

        return users;
    }

    public bool Update(User user)
    {
        using var con = new NpgsqlConnection(_configuration.DatabaseConnection());
        con.Open();

        var query = "update users set username = @username, password = @passwd, role= @role where id=@userid";
        using var cmd = new NpgsqlCommand(query, con);
        cmd.Parameters.AddWithValue("username", user.Username);
        cmd.Parameters.AddWithValue("passwd", user.Password);
        cmd.Parameters.AddWithValue("role", user.Role);
        cmd.Parameters.AddWithValue("userid", user.Id);

        var rows = cmd.ExecuteNonQuery();
        return rows > 0;
    }
}


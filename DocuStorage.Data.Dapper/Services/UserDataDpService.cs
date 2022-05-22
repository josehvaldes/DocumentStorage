namespace DocuStorage.Data.Dapper.Services;
using DocuStorage.Common.Data.Model;
using DocuStorage.Common.Data.Services;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;

public class UserDataDpService : IUserDataService
{
    private ISqlDataProvider _dataProvider;

    public UserDataDpService(ISqlDataProvider dataProvider)
    {
        _dataProvider = dataProvider;
    }

    public User Create(User user)
    {

        using var con = _dataProvider.GetConnection();
        con.Open();

        var id = con.ExecuteScalar<int>("insert into users(username, password, role) values (@username, @password, @role) RETURNING id", user );
        if (id > 0)
        {
            user.Id = id;
        }
        else
        {
            throw new NpgsqlException("Invalid ID returned");
        }

        return user;

    }

    public void Delete(int userId)
    {
        using var con = _dataProvider.GetConnection();
        con.Open();

        int delRows = con.Execute("delete from users where id = @userid", new { userid = userId });
        if (delRows <= 0)
        {
            throw new Exception("Row not found");
        }
    }

    public User? Get(int id)
    {
        using var con = _dataProvider.GetConnection();
        con.Open();

        var user = con.Query<User>("select * from get_user(@userId)", new { userId = id }).FirstOrDefault();
        return user;
    }

    public User? Get(User userparams)
    {
        using var con = _dataProvider.GetConnection();
        con.Open();

        var user = con.Query<User>("select * from get_user(@username, @password)", 
            new { username = userparams.Username, password = userparams.Password } ).FirstOrDefault();

        return user;
    }

    public List<User> GetAll()
    {
        using var con = _dataProvider.GetConnection();
        con.Open();

        var list = con.Query<User>("select * from get_all_users()").ToList();

        return list;
    }

    public bool Update(User user)
    {
        using var con = _dataProvider.GetConnection();
        con.Open();
            
        var rows = con.Execute(
            "update users set username = @username, password = @password, role= @role where id=@id", user);

        return rows > 0;
    }
}

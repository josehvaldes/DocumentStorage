﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using Microsoft.Extensions.Configuration;
using DocuStorate.Data.Model;
using DocuStorage.Common;

namespace DocuStorate.Data.Services
{
    public class UserDataService : IUserDataService
    {
        public User Create(User user) 
        {
            using var con = new NpgsqlConnection(Configuration.DatabaseConnection());
            con.Open();

            var query = "insert into Users(username, password, role) values (@username, @passwd, @role) RETURNING id";

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
            using var con = new NpgsqlConnection(Configuration.DatabaseConnection());
            con.Open();
            var query = "delete from users where id = @userid";
            using var cmd = new NpgsqlCommand(query, con);
            cmd.Parameters.AddWithValue("userid", userId);
            var val = cmd.ExecuteNonQuery();
        }

        public User? Get(int id) 
        {
            using var con = new NpgsqlConnection(Configuration.DatabaseConnection());
            con.Open();

            var query = "select * from GetUser(@user_id)";

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
            using var con = new NpgsqlConnection(Configuration.DatabaseConnection());
            con.Open();

            var query = "select * from GetUser(@username, @passwd)";

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

            var cs = Configuration.DatabaseConnection();

            using (var con = new NpgsqlConnection(cs)) 
            {
                con.Open();

                var query = "select * from GetAllUsers()";

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

        public User Update(User user)
        {
            using var con = new NpgsqlConnection(Configuration.DatabaseConnection());
            con.Open();

            var query = "update users set username = @username, password = @passwd, role= @role where id=@userid";
            using var cmd = new NpgsqlCommand(query, con);
            cmd.Parameters.AddWithValue("username", user.Username);
            cmd.Parameters.AddWithValue("passwd", user.Password);
            cmd.Parameters.AddWithValue("role", user.Role);
            cmd.Parameters.AddWithValue("userid", user.Id);

            var val = cmd.ExecuteNonQuery();

            return user;
        }
    }
}

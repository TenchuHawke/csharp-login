using System.Data;
using Dapper;
using Login.Models;
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;

namespace Login.Factories
{
    public class UserFactory : IFactory<User>
    {
        private readonly IOptions<MySqlOptions> MySqlConfig;
        
        public UserFactory(IOptions<MySqlOptions> config)
        {
            MySqlConfig = config;
        }

        internal IDbConnection Connection
        {
            get { return new MySqlConnection(MySqlConfig.Value.ConnectionString); }
        }

        public void Add(User Item)
        {
            using(IDbConnection dbConnection = Connection)
            {
                string Query = "INSERT into users (first_name, username, last_name, email, password, created_at, modified_at, last_on, birthday) VALUES (@first_name, @username, @last_name, @email, @password, NOW(), NOW(), NOW(), @birthday)";
                dbConnection.Open();
                dbConnection.Execute(Query, Item);
            }
        }

        public User GetLatestUser()
        {
            using(IDbConnection dbConnection = Connection)
            {
                string Query = "SELECT * FROM users ORDER BY user_id DESC LIMIT 1";
                dbConnection.Open();
                return dbConnection.QuerySingleOrDefault<User>(Query);
            }
        }

        public User GetUserById(int Id)
        {
            using(IDbConnection dbConnection = Connection)
            {
                string Query = $"SELECT * FROM users WHERE user_id = {Id}";
                dbConnection.Open();
                return dbConnection.QuerySingleOrDefault<User>(Query);
            }
        }

        public User GetuserByUsername(string username)
        {
            using(IDbConnection dbConnection = Connection)
            {
                string Query = $"SELECT * FROM users WHERE username = '{username}'";
                dbConnection.Open();
                return dbConnection.QuerySingleOrDefault<User>(Query);
            }
        }
    }
}
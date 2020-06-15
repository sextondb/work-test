using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using api.Models;
using Dapper;
using Microsoft.AspNetCore.Mvc;

namespace api.Repositories
{
    public class UserRepository : IDisposable
    {
        private readonly IDbConnection connection;

        public UserRepository(IDbConnection connection)
        {
            this.connection = connection;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await connection.QueryAsync<User>("select * from users");
        }

        public async Task<User> GetByUsernameAsync(string username)
        {
            return await connection.QueryFirstAsync<User>("select * from users where username = @username", new { username });
        }

        public async Task<User> GetAsync(int id)
        {
            return await connection.QueryFirstAsync<User>("select * from users where id = @id", new { id });
        }

        public void Dispose()
        {
            connection.Dispose();
        }
    }
}

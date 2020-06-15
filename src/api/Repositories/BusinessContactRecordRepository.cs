using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using Dapper;

namespace api.Repositories
{
    public class BusinessContactRecordRepository
    {
        private readonly IDbConnection connection;

        public BusinessContactRecordRepository(IDbConnection connection)
        {
            this.connection = connection;
        }

        public async Task<IEnumerable<BusinessContactRecord>> GetAllAsync(int userId)
        {
            var sql = @"
                SELECT UserId
                    ,Id
                    ,Name
                    ,Email
                    ,AddressLine1 as Line1
                    ,AddressLine2 as Line2
                    ,AddressCity as City
                    ,AddressStateOrProvince as StateOrProvince
                    ,AddressPostalCode as PostalCode
                FROM records
                WHERE userId = @userid;
            ";

            var records = await connection.QueryAsync<BusinessContactRecord, BusinessContactAddress, BusinessContactRecord>(sql, (record, address) =>
                {
                    record.Address = address;
                    return record;
                },
                param: new { userId },
                splitOn: "Line1"
            );

            return records;
        }

        public async Task DeleteAsync(int userId, int id)
        {
            var sql = @"
                DELETE
                FROM records
                WHERE userId = @userid
                    and Id = @id;
            ";

            await connection.ExecuteAsync(sql, new { userId, id });
        }

        public async Task<BusinessContactRecord> GetAsync(int userId, int id)
        {
            var sql = @"
                SELECT UserId
                    ,Id
                    ,Name
                    ,Email
                    ,AddressLine1 as Line1
                    ,AddressLine2 as Line2
                    ,AddressCity as City
                    ,AddressStateOrProvince as StateOrProvince
                    ,AddressPostalCode as PostalCode
                FROM records
                WHERE userId = @userid
                    and Id = @id;
            ";

            var record = (await connection.QueryAsync<BusinessContactRecord, BusinessContactAddress, BusinessContactRecord>(sql, (record, address) =>
            {
                record.Address = address;
                return record;
            },
                param: new { userId, id },
                splitOn: "Line1"
            )).FirstOrDefault();

            return record;
        }

        public async Task UpdateAsync(int userId, BusinessContactRecord record)
        {
            var sql = @"
                UPDATE [dbo].[Records]
                SET  [Name] = @Name
                    ,[Email] = @Email
                    ,[AddressLine1] = @Line1
                    ,[AddressLine2] = @Line2
                    ,[AddressCity] = @City
                    ,[AddressStateOrProvince] = @StateOrProvince
                    ,[AddressPostalCode] = @PostalCode
                WHERE UserId = @UserId
                    AND Id = @ID
            ";

            var id = await connection.ExecuteAsync(sql, new
            {
                record.UserId,
                record.Id,
                record.Name,
                record.Email,
                record.Address.Line1,
                record.Address.Line2,
                record.Address.City,
                record.Address.StateOrProvince,
                record.Address.PostalCode
            });

        }

        public async Task<int> InsertAsync(int userId, BusinessContactRecord record)
        {
            var sql = @"
                INSERT INTO [dbo].[Records]
                       ([UserId]
                       ,[Name]
                       ,[Email]
                       ,[AddressLine1]
                       ,[AddressLine2]
                       ,[AddressCity]
                       ,[AddressStateOrProvince]
                       ,[AddressPostalCode])
                OUTPUT INSERTED.Id
                VALUES (
                     @UserId
                    ,@Name
                    ,@Email
                    ,@Line1
                    ,@Line2
                    ,@City
                    ,@StateOrProvince
                    ,@PostalCode
                );
                
            ";

            var id = await connection.QuerySingleAsync<int>(sql, new
            {
                record.UserId,
                record.Name,
                record.Email,
                record.Address.Line1,
                record.Address.Line2,
                record.Address.City,
                record.Address.StateOrProvince,
                record.Address.PostalCode
            });

            return id;
        }
    }
}

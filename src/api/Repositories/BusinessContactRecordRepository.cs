using System;
using System.Collections.Generic;
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

        public async Task<PagedResult<IEnumerable<BusinessContactRecord>>> GetAllPagedAsync(int userId, int pageSize, int page)
        {
            if(pageSize <= 0 || page < 0)
            {
                throw new ArgumentException();
            }

            var rowOffset = pageSize * (page - 1);

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
                ORDER BY Id
                OFFSET @rowOffset ROWS
                FETCH NEXT @pageSize ROWS ONLY;
                
                SELECT count(*)
                FROM records
                WHERE userId = @userid;
            ";


            var multiResult = await connection.QueryMultipleAsync(sql, new { userId, rowOffset, pageSize });
            var records = multiResult.Read<BusinessContactRecord, BusinessContactAddress, BusinessContactRecord>((record, address) =>
            {
                record.Address = address;
                return record;
            }, splitOn: "Line1");

            var totalCount = multiResult.ReadFirst<int>();

            return new PagedResult<IEnumerable<BusinessContactRecord>>() { Data = records, Page = page, PageSize = pageSize, TotalCount = totalCount};
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

        public async Task<int> UpdateAsync(int userId, int id, BusinessContactRecord record)
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
                WHERE UserId = @userId
                    AND Id = @id
            ";

            var rowsUpdated = await connection.ExecuteAsync(sql, new
            {
                userId,
                id,
                record.Name,
                record.Email,
                record.Address.Line1,
                record.Address.Line2,
                record.Address.City,
                record.Address.StateOrProvince,
                record.Address.PostalCode
            });

            return rowsUpdated;
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
                     @userId
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
                userId,
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

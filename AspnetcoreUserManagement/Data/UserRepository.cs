using AspnetcoreUserManagement.Models.Entities;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace AspnetcoreUserManagement.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly string _connectionString;
        //ToDo - modify the repository with transactions so that everything is under one transaction
        public UserRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection") 
                ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        }

        private IDbConnection Connection => new SqlConnection(_connectionString);

        public async Task DeleteAllAsync()
        {
            const string sql1 = "DELETE FROM Addresses";
            const string sql2 = "DELETE FROM Users";

            using var con = Connection;

            await con.ExecuteAsync(sql1);
            await con.ExecuteAsync(sql2);
        }

        public async Task InsertUserAsync(User user)
        {
            const string sql = @"
            INSERT INTO Users
            (Name, Username, Password, Email, Phone, Website, Note, IsActive, CreatedAt)
            VALUES
            (@Name, @Username, @Password, @Email, @Phone, @Website, @Note, @IsActive, @CreatedAt);

            SELECT CAST(SCOPE_IDENTITY() as int);
        ";

            using var con = Connection;

            // Insert user and get generated Id
            int userId = await con.ExecuteScalarAsync<int>(sql, new
            {
                user.Name,
                user.Username,
                user.Password,
                user.Email,
                user.Phone,
                user.Website,
                user.Note,
                user.IsActive,
                user.CreatedAt
            });

            // Insert address (one address per user)
            foreach (var addr in user.Addresses)
            {
                const string sqlAddr = @"
                INSERT INTO Addresses
                (Street, Suite, City, Zipcode, Lat, Lng, UserId)
                VALUES
                (@Street, @Suite, @City, @Zipcode, @Lat, @Lng, @UserId)
            ";

                addr.UserId = userId;

                await con.ExecuteAsync(sqlAddr, addr);
            }
        }
    }
}

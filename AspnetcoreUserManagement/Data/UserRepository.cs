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
            using (var con = new SqlConnection(_connectionString))
            {
                await con.OpenAsync();

                using (var transaction = con.BeginTransaction())
                {
                    try
                    {
                        await con.ExecuteAsync("DELETE FROM Addresses", transaction: transaction);
                        await con.ExecuteAsync("DELETE FROM Users", transaction: transaction);

                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
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
        public async Task ReplaceAllUsersAsync(List<User> users)
        {
            using (var con = new SqlConnection(_connectionString))
            {
                await con.OpenAsync();

                using (var transaction = con.BeginTransaction())
                {
                    try
                    {
                        // 1. Delete old data
                        await con.ExecuteAsync("DELETE FROM Addresses", transaction: transaction);
                        await con.ExecuteAsync("DELETE FROM Users", transaction: transaction);

                        // 2. Insert users
                        foreach (var user in users)
                        {
                            // Insert user
                            var userId = await con.ExecuteScalarAsync<int>(
                                @"INSERT INTO Users
                          (Name, Username, Password, Email, Phone, Website, Note, IsActive, CreatedAt)
                          VALUES
                          (@Name, @Username, @Password, @Email, @Phone, @Website, @Note, @IsActive, @CreatedAt);
                          SELECT CAST(SCOPE_IDENTITY() as int);",
                                user,
                                transaction
                            );

                            // Insert addresses
                            foreach (var addr in user.Addresses)
                            {
                                addr.UserId = userId;

                                await con.ExecuteAsync(
                                    @"INSERT INTO Addresses
                              (Street, Suite, City, Zipcode, Lat, Lng, UserId)
                              VALUES
                              (@Street, @Suite, @City, @Zipcode, @Lat, @Lng, @UserId)",
                                    addr,
                                    transaction
                                );
                            }
                        }

                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;  // bubble up the error
                    }
                }
            }
        }


    }
}

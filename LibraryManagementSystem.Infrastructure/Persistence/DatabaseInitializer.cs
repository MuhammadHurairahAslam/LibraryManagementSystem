using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Infrastructure.Persistence
{
    public class DatabaseInitializer
    {
        private readonly string _connectionString;
        private readonly string _scriptsRootPath;

        public DatabaseInitializer(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")!;
            _scriptsRootPath = Path.Combine(AppContext.BaseDirectory, "Scripts");
        }

        public async Task InitializeAsync()
        {
            var builder = new SqlConnectionStringBuilder(_connectionString);
            string databaseName = builder.InitialCatalog;
            string masterConnection = builder.ConnectionString.Replace(builder.InitialCatalog, "master");

            // Step 1: Ensure database exists
            using (var masterConn = new SqlConnection(masterConnection))
            {
                await masterConn.OpenAsync();
                string createDbQuery = $"IF DB_ID('{databaseName}') IS NULL CREATE DATABASE [{databaseName}];";
                await masterConn.ExecuteAsync(createDbQuery);
            }

            using var dbConnection = new SqlConnection(_connectionString);
            await dbConnection.OpenAsync();

            // Step 2: Check and create tables
            var tablesPath = Path.Combine(_scriptsRootPath, "Tables");
            foreach (var file in Directory.GetFiles(tablesPath, "*.sql"))
            {
                var tableName = Path.GetFileNameWithoutExtension(file);
                var exists = await dbConnection.ExecuteScalarAsync<int>(
                    "SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = @TableName",
                    new { TableName = tableName });

                if (exists == 0)
                {
                    var sql = await File.ReadAllTextAsync(file);
                    await dbConnection.ExecuteAsync(sql);

                    // Optional: run seeding if table was just created
                    var seedFile = Path.Combine(_scriptsRootPath, "Seedings", tableName + "Seed.sql");
                    if (File.Exists(seedFile))
                    {
                        var seedSql = await File.ReadAllTextAsync(seedFile);
                        await dbConnection.ExecuteAsync(seedSql);
                    }
                }
            }

            // Step 3: Check and create stored procedures
            var spPath = Path.Combine(_scriptsRootPath, "StoredProcedures");
            foreach (var file in Directory.GetFiles(spPath, "*.sql"))
            {
                var spName = Path.GetFileNameWithoutExtension(file);
                var exists = await dbConnection.ExecuteScalarAsync<int>(
                    "SELECT COUNT(*) FROM sys.procedures WHERE name = @SpName",
                    new { SpName = spName });

                if (exists == 0)
                {
                    var spSql = await File.ReadAllTextAsync(file);
                    await dbConnection.ExecuteAsync(spSql);
                }
            }
        }
    }
}

using System.Data.Common;
using Microsoft.Data.Sqlite;
using System.IO;
using Microsoft.EntityFrameworkCore;
using UIDesign.utils;
namespace UIDesign.Helper
{
    /// <summary>
    /// 数据库管理类
    /// </summary>
    public class SQLiteHelper
    {
        private static readonly string _path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Database");

        public static void InitializeDatabase()
        {
            string _databaseFilePath = Path.Combine(_path, "myDB.db");

            if (!Directory.Exists(_path))
            {
                Directory.CreateDirectory(_path);
            }

            if (!File.Exists(_databaseFilePath))
            {
                //SqliteConnection.CreateFile(_databaseFilePath);

                // 创建表
                using (var connection = new SqliteConnection($"Data Source={_databaseFilePath};"))
                {
                    connection.Open();

                    string createTableQuery = @"
            CREATE TABLE IF NOT EXISTS DBModels (
                DbInstanceUID TEXT NOT NULL,
                Number TEXT NOT NULL,
                Price TEXT NOT NULL,
                Time TEXT NOT NULL
            );";

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = createTableQuery;
                        command.ExecuteNonQuery();
                    }
                }

            }
            else
            {
                Console.WriteLine("Database already exists.");
            }
        }

        /// <summary>
        /// 数据库连接方法
        /// </summary>
        /// <returns></returns>
        public static DbConnection GetDbConnection()
        {
            string _databaseFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Database", "myDB.db");

            // 确保数据库文件夹存在
            EnsureDatabaseFolderExists();

            // 返回 SqliteConnection 对象
            return new SqliteConnection($"Data Source={_databaseFilePath};");
        }

        private static void EnsureDatabaseFolderExists()
        {
            // 确保文件夹存在，如果不存在则创建
            if (!Directory.Exists(_path))
            {
                Directory.CreateDirectory(_path);
            }
        }

        public static Glh_DBContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<Glh_DBContext>()
                .UseSqlite($"Data Source={Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Database", "myDB.db")}")
                .Options;

            return new Glh_DBContext(options);
        }

    }
}

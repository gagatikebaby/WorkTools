using Microsoft.EntityFrameworkCore;
using System.Data.Common;
//using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using UIDesign.Helper;

namespace UIDesign.utils
{
    public class DBManger
    {


        /// <summary>
        /// 静态全局变量
        /// </summary>
        public static readonly DBManger Instance = new DBManger();
        static object _lock = new object();
        private DBManger()
        {

        }

        /// <summary>
        /// 插入一行
        /// </summary>
        /// <typeparam name="T">模型类型</typeparam>
        /// <param name="model">模型实例</param>
        /// <returns>影响行数</returns>
        // Insert Method
        public int Insert<T>(T model) where T : class, new()
        {
            int success = 0;

            // 确保线程安全
            lock (_lock)
            {
                // 获取 SQLite 连接
                using (var connection = SQLiteHelper.GetDbConnection())
                {
                    // 配置 Entity Framework Core 使用 SQLite
                    var options = new DbContextOptionsBuilder<Glh_DBContext>()
                        .UseSqlite(connection) // Use SQLite with the given connection
                        .Options;

                    // 使用 DBContext 执行数据库操作
                    using (var context = new Glh_DBContext(options))
                    {
                        context.Set<T>().Add(model);
                        success = context.SaveChanges();
                    }
                }
            }

            return success;
        }

        /// <summary>
        /// 查询（条件传空返回结果目标表的所有记录）
        /// </summary>
        /// <typeparam name="T">模型类型</typeparam>
        /// <param name="exp">表达式</param>
        /// <returns>返回影响记录</returns>
        public IEnumerable<T> Select<T>(Expression<Func<T, bool>> exp = null)
            where T : class, new()
        {
            return Filter(exp);
        }

        /// <summary>
        /// 内部查询使用方法
        /// </summary>
        /// <typeparam name="T">模型类型</typeparam>
        /// <param name="exp">查询表达式</param>
        /// <returns>返回IQueryable类型</returns>
        private IQueryable<T> Filter<T>(Expression<Func<T, bool>> exp = null)
            where T : class, new()
        {
            var context = SQLiteHelper.GetDbContext();
            var query = context.Set<T>().AsNoTracking();
            return exp != null ? query.Where(exp) : query;
        }

        /// <summary>
        /// 删除一行
        /// </summary>
        /// <typeparam name="T">模型类型</typeparam>
        /// <param name="entity">实体类</param>
        /// <returns>影响行数</returns>
        public int Delete<T>(T model) where T : class, new()

        {
            int success = 0;

            // 确保线程安全
            lock (_lock)
            {
                // 获取 SQLite 连接
                using (var connection = SQLiteHelper.GetDbConnection())
                {
                    // 配置 Entity Framework Core 使用 SQLite
                    var options = new DbContextOptionsBuilder<Glh_DBContext>()
                        .UseSqlite(connection) // Use SQLite with the given connection
                        .Options;

                    // 使用 DBContext 执行数据库操作
                    using (var context = new Glh_DBContext(options))
                    {
                        context.Set<T>().Remove(model);
                        success = context.SaveChanges();
                    }
                }
            }

            return success;
        }

    }


}


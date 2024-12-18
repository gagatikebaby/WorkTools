using System;
using System.Collections.Generic;
//using System.Data.Common;
//using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIDesign.Model;
using SQLite.CodeFirst;
using Microsoft.EntityFrameworkCore;

namespace UIDesign.utils
{

    public class Glh_DBContext : DbContext
    {
        public Glh_DBContext(DbContextOptions<Glh_DBContext> options) : base(options) { }

        // 添加 DbSet 对象
        public DbSet<DBModel> DBModels { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // 可选的：配置模型映射
            modelBuilder.Entity<DBModel>()
                        .ToTable("DBModels"); // 配置表名为 DBModels
            modelBuilder.Entity<DBModel>()
                .HasKey(e => e.DbInstanceUID); // 指定 Id 作为主键
            base.OnModelCreating(modelBuilder);
        }
    }


}


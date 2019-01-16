using Restaurant.Models;
using Microsoft.EntityFrameworkCore;

namespace Restaurant.Service
{
    // 数据库上下文
    public class DataContext : DbContext
    {
        // 数据库设置
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        // 数据库表
        public DbSet<MenuItemSQLModel> Menus { get; set; }
        // 待处理的数据
        public DbSet<OrderSQLModel> ToBeProcessed { get; set; }
        // 已经处理的数据
        public DbSet<CompleteOrderSQLModel> Complete { get; set; }
    }
}
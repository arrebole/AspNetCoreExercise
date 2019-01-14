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
        public DbSet<MenuItemModel> Menus { get; set; }
    }
}
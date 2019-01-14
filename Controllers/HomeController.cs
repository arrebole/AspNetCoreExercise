using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restaurant.Models;
using Restaurant.Service;
namespace Restaurant.Controllers
{
    // 主页控制器
    public class HomeController : Controller
    {
        // 存放页面需要的数据
        private HomeViewModel viewDB;
        // 构造函数——从数据库中读取并整理视图需要的数据
        public HomeController(DataContext dbContext)
        {
            viewDB = new HomeViewModel();

            // 如果数据库为空，则添加默认数据
            this.initDB(dbContext);
            // 从数据库读取数据整理到viewDB
            this.organizeData(dbContext);
        }

        // 初始化menu数据库
        private void initDB(DataContext dbContext)
        {
            // 如果数据库为空，则添加默认数据
            if (dbContext.Menus.Count() == 0)
            {
                dbContext.Menus.Add(new MenuItemModel { Id = 100, Group = 1, Name = "米饭", Price = 1.0, SalesVolume = 0, });
                dbContext.Menus.Add(new MenuItemModel { Id = 200, Group = 2, Name = "糖醋里脊", Price = 5.0, SalesVolume = 0, });
                dbContext.Menus.Add(new MenuItemModel { Id = 300, Group = 3, Name = "紫菜汤", Price = 2.0, SalesVolume = 0, });
                dbContext.SaveChanges();
            }
        }

        // 导出整理
        private void organizeData(DataContext dbContext)
        {
            foreach (var item in dbContext.Menus.AsNoTracking().ToList())
            {
                // 整理分组
                switch (item.Group)
                {
                    case 1:
                        viewDB.Rice.Add(item);
                        break;
                    case 2:
                        viewDB.Dish.Add(item);
                        break;
                    case 3:
                        viewDB.Soup.Add(item);
                        break;
                }
            }
        }




        /*----------------------处理请求----------------------------------*/
        // 处理首页请求
        [HttpGet]
        public IActionResult Index()
        {
            // 创建视图数据交给视图处理
            return View(viewDB);
        }

        // 默认处理错误
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

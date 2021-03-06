﻿using System;
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
        private DataContext _dbContext;
        // 构造函数——从数据库中读取并整理视图需要的数据
        public HomeController(DataContext dbContext)
        {
            viewDB = new HomeViewModel();
            // 将数据上下文备份 挂载到类内 _dbContext中
            _dbContext = dbContext;
            // 如果数据库上下文为空，则添加默认数据
            this.initDB(dbContext);
            // 从数据库上下文 读取数据整理到viewDB
            this.organizeData(dbContext);
        }

        // 初始化menu数据库
        private void initDB(DataContext dbContext)
        {
            // 如果数据库为空，则添加默认数据
            if (dbContext.Menus.Count() == 0)
            {
                dbContext.Menus.Add(new MenuItemSQLModel { Group = 1, Name = "米饭", Price = 1.0, SalesVolume = 0, });
                dbContext.Menus.Add(new MenuItemSQLModel { Group = 2, Name = "糖醋里脊", Price = 5.0, SalesVolume = 0, });
                dbContext.Menus.Add(new MenuItemSQLModel { Group = 3, Name = "紫菜汤", Price = 2.0, SalesVolume = 0, });
                dbContext.SaveChanges();
            }
        }

        // 整理交给views的数据
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
        // 首页视图页面处理
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

        
        // Post请求提交订单
        [HttpPost]
        public IActionResult ApplyOrder([FromBody]PostOrder postOrder)
        {
            // 此次订单的价格
            double iMoney = 0;
            // 订单的内容
            var detail = "";

            // ------------数据库改动增加销量------------------------
            foreach (var item in postOrder.MenuList)
            {
                var it = _dbContext.Menus.FirstOrDefault(t => t.Id == item.Id);
                // 计算此次订单价格
                iMoney += (it.Price * (double)item.Count);
                // 计算此次订单价格的内容
                detail += it.Name + "X" + item.Count + " ";

                // 从数据库中修改销量
                it.SalesVolume += item.Count;

                _dbContext.Menus.Update(it);
                _dbContext.SaveChanges();
            }
            // ------------数据库改动生成待处理订单-----------------------

            var toBeProcessed = new OrderSQLModel
            {
                Time = postOrder.Time,
                Seat = postOrder.Seat,
                Money = iMoney,
                Detail = detail,
            };

            _dbContext.ToBeProcessed.Add(toBeProcessed);
            _dbContext.SaveChanges();

            //----------------------------------------------------------
            return Json(new { code = "Ok" });
        }

    }
}

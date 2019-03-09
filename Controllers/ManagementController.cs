using System;
using System.Text;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Models;
using Microsoft.EntityFrameworkCore;
using Restaurant.Service;
using System.Text.RegularExpressions;

namespace Restaurant.Controllers
{
    /**
     *
     *  管理页面视图控制
     *  get: /Management/AddMenu
     *  get: /Management/Complete
     *  get: /Management/ToBeProcessed
     */
    public class ManagementController : Controller
    {

        // -----------------构造函数 数据库上下文处理--------------------------------
        private DataContext _dbContext;

        public ManagementController(DataContext dbContext)
        {
            this._dbContext = dbContext;
        }


        // ----------添加菜单页面-------------------------------
        [HttpGet]
        public IActionResult UpdateMenu()
        {

            return View();
        }

        [HttpPost]
        public IActionResult UpdateMenuHandle([FromBody]UpdateMenuInfo updateItem)
        {
            // 分析分组
            var group = 1;
            switch (updateItem.UpdateMenuGroup)
            {
                case "主食":
                    group = 1;
                    break;
                case "主菜":
                    group = 2;
                    break;
                case "汤类":
                    group = 3;
                    break;
                default:
                    return Json(new { Message = "Fail" });
            }

            //-------------------------------------------------------------
            if (updateItem.UpdateMenuFunc == "删除")
            {
                var i = _dbContext.Menus.FirstOrDefault(t => t.Name == updateItem.UpdateMenuName);
                if (i == null)
                {
                    return Json(new { Message = "Fail" });
                }
                _dbContext.Menus.Remove(i);
                _dbContext.SaveChanges();
                return Json(new { Message = "Ok" });
            }
            //---------------------------------------------------------------
            else if (updateItem.UpdateMenuFunc == "增加")
            {
                var i = _dbContext.Menus.FirstOrDefault(t => t.Name == updateItem.UpdateMenuName);
                if (i != null)
                {
                    return Json(new { Message = "Fail" });
                }

                // 数据库中添加数据
                _dbContext.Menus.Add(new MenuItemSQLModel
                {
                    Name = updateItem.UpdateMenuName,
                    SalesVolume = 0,
                    Group = group,
                    Price = double.Parse(updateItem.UpdateMenuPrice)
                });
                _dbContext.SaveChanges();
                return Json(new { Message = "Ok" });
            }
            // -------------------------------------------------------
            else if (updateItem.UpdateMenuFunc == "修改")
            {
                var i = _dbContext.Menus.FirstOrDefault(t => t.Name == updateItem.UpdateMenuName);
                if (i == null)
                {
                    return Json(new { Message = "Fail" });
                }
                i.Name = updateItem.UpdateMenuName;
                i.Group = group;
                i.Price = double.Parse(updateItem.UpdateMenuPrice);
                _dbContext.Menus.Update(i);
                _dbContext.SaveChanges();
                return Json(new { Message = "Ok" });
            }
            return Json(new { Message = "Fail" });


        }




        //------------------- 获取已处理订单------------------------------------------
        [HttpGet]
        public IActionResult Complete()
        {
            var _CompleteList = _dbContext.Complete.AsNoTracking().ToList();
            // 统计价格
            double _TotalRevenue = 0;
            foreach (var item in _CompleteList)
            {
                _TotalRevenue += item.Money;
            }

            // 统计销量
            var _moneyMap = new Dictionary<string, double>();
            foreach (var item in _CompleteList)
            {

                var dateLongTime = item.EndTime;
                var subString = Regex.Match(dateLongTime, @"^([0-9]{4})\/([0-9]+)");
                // 如果不存在
                if (!_moneyMap.ContainsKey(subString.Value))
                {
                    _moneyMap.Add(subString.Value, item.Money);
                }
                else
                {
                    _moneyMap[subString.Value] += item.Money;
                }

            }

            // 返回数据
            return View(new CompleteViewModel
            {
                TotalRevenue = _TotalRevenue,
                CompleteList = _CompleteList,
                NowData = DateTime.Now.ToLocalTime().ToString(),
                moneyMap = _moneyMap,
            });
        }

        // -----------------获取待处理订单--------------------------------------------------------
        [HttpGet]
        public IActionResult ToBeProcessed()
        {

            return View(new ToBeProcessedViewModel
            {
                ToBeProcessedList = _dbContext.ToBeProcessed.AsNoTracking().ToList()
            });
        }

        // -------------------处理订单-----------------------------------
        [HttpPost]
        public IActionResult GoProcess([FromBody]OrderProcess po)
        {
            var i = _dbContext.ToBeProcessed.FirstOrDefault(t => t.Id == po.Id);
            if (i == null)
            {
                return Json(new { code = "Fail" });
            }
            // 从待处理订单从去除
            _dbContext.ToBeProcessed.Remove(i);
            _dbContext.SaveChanges();

            // 添加到已处理订单中
            _dbContext.Complete.Add(new CompleteOrderSQLModel
            {
                BeginTime = i.Time,
                EndTime = DateTime.Now.ToLocalTime().ToString(),
                Detail = i.Detail,
                Money = i.Money,
                Seat = i.Seat,
            });

            _dbContext.SaveChanges();
            return Json(new { code = "Ok" });
        }

    }

}
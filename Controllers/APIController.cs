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

namespace Restaurant.Controllers
{
    [Produces("application/json")]
    [Route("api/[Controller]")]
    public class ApplyOrderController : Controller
    {

        // --------------------数据库上下文----------------------------------
        private DataContext _dbContext;

        public ApplyOrderController(DataContext dbContext)
        {
            _dbContext = dbContext;
        }
        //----------------------------------------------------------

        [HttpPost]
        public IActionResult Index([FromBody]PostOrder postOrder)
        {
            // 此次订单的价格
            double iMoney = 0;
            // 订单的内容
            var detail = "";

            // ------------数据库改动增加销量------------------------
            foreach (var item in postOrder.menuList)
            {
                var it = _dbContext.Menus.FirstOrDefault(t => t.Id == item.id);
                // 计算此次订单价格
                iMoney += (it.Price * (double)item.count);
                // 计算此次订单价格的内容
                detail += it.Name + "X" + item.count + " ";

                // 从数据库中修改销量
                it.SalesVolume += item.count;

                _dbContext.Menus.Update(it);
                _dbContext.SaveChanges();
            }
            // ------------数据库改动生成待处理订单-----------------------
            
            var toBeProcessed = new OrderSQLModel
            {
                Time = postOrder.time,
                Seat = postOrder.seat,
                Money = iMoney,
                Detail = detail,
            };

            _dbContext.ToBeProcessed.Add(toBeProcessed);
            _dbContext.SaveChanges();

            //----------------------------------------------------------
            return Json(new { code = "Success" });
        }



    }


}
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Models;
using Restaurant.Service;
using System;

namespace Restaurant.Controllers
{
    public class AccountController : Controller
    {
        // 相应视图
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }


        // 登录处理---------------------------------------------------
        [HttpPost]
        public IActionResult Login([FromBody]LoginPostInfo info)
        {
            // 验证用户名密码是否正确
            if (info.UserName == AdminAccount.UserName && info.PassWord == AdminAccount.PassWord)
            {
                // 如果正确则返回cookie
                return Json(new { token = AdminAccount.Token });
            }
            return Json(new { code = "error" });
        }
    }
}
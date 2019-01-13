using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Models;

namespace Restaurant.Controllers
{
    /**
     *
     *  管理页面路由控制
     *  get: /Management/AddMenu
     *  get: /Management/Complete
     *  get: /Management/ToBeProcessed
     */
    public class ManagementController : Controller
    {
        public IActionResult AddMenu()
        {
            return View();
        }

        public IActionResult Complete()
        {
            return View();
        }

        public IActionResult ToBeProcessed()
        {
            return View();
        }

    }
}
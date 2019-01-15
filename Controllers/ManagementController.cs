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
     *  管理页面视图控制
     *  get: /Management/AddMenu
     *  get: /Management/Complete
     *  get: /Management/ToBeProcessed
     */
    public class ManagementController : Controller
    {
        [HttpGet]
        public IActionResult AddMenu()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Complete()
        {
            return View();
        }
        [HttpGet]
        public IActionResult ToBeProcessed()
        {
            return View();
        }

    }

}
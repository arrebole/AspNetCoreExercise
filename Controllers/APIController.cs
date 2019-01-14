using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Models;

namespace Restaurant.Controllers
{
    [Produces("application/json")]
    [Route("api/[Controller]")]
    public class applyOrderController : Controller
    {
        [HttpPost]
        public ActionResult<Success> Index([FromBody]Order order)
        {
            return new Success { code = "Success" };
        }
    }
    public class Success
    {
        public string code { get; set; }
    }
}
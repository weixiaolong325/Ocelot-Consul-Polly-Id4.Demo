using ConsulAndOcelot.Demo.ServerB.Utils;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsulAndOcelot.Demo.ServerB.Controllers
{
    [Route("api/[controller]/[action]")]
    public class OrderController : Controller
    {
        private readonly OrderService _orderService;
        public OrderController(OrderService orderService)
        {
            _orderService = orderService;
        }
        public IActionResult CreateOrder()
        {
            string result = _orderService.CreateOrder();
            return Content(result);
        }
    }
}

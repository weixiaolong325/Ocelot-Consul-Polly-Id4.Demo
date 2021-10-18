using ConsulAndOcelot.Demo.ServerA.Models;
using ConsulAndOcelot.Demo.ServerA.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ConsulAndOcelot.Demo.ServerA.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ConsulHelper _consulHelper;

        public HomeController(ILogger<HomeController> logger, ConsulHelper consulHelper)
        {
            _logger = logger;
            _consulHelper = consulHelper;
        }

        public IActionResult Index()
        {
            ///获取api服务地址
            var domain = _consulHelper.GetApiDomain();
            ViewBag.domain = domain;
           // string apiResult = WebApiHelper.Get($"{domain}/Home/Test");
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

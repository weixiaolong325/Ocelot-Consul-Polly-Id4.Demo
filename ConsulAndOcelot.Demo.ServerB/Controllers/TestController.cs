using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ConsulAndOcelot.Demo.ServerB.Controllers
{
    [Route("api/[controller]/[action]")]
    public class TestController : Controller
    {
        private IConfiguration _configuration;
        public TestController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IActionResult GetName()
        {
            string port = _configuration["port"];
            return Json($"端口:{port}，姓名:张三");
        }
        public IActionResult GetEx()
        {
            string port = _configuration["port"];
            throw new Exception($"端口:{port}，处理异常");
        }
        public IActionResult GetSleep()
        {
            string port = _configuration["port"];

            //线程睡眠6s
            Thread.Sleep(6000);
            return Json($"端口:{port}，睡眠6s后返回");
        }
    }
}

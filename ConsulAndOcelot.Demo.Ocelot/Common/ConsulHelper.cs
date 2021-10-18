using Consul;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsulAndOcelot.Demo.Ocelot
{
    public static class ConsulHelper
    {
        public static void ConsulRegist(this IConfiguration configuration)
        {
            ConsulClient client = new ConsulClient(c=> {
                c.Address = new Uri("http://localhost:8500");
                c.Datacenter = "dc1";
            });
            string domain = configuration["domain"];
            client.Agent.ServiceRegister(new AgentServiceRegistration()
            {
                ID = "service" + Guid.NewGuid(), //唯一的
                Name = "网关站点", //组名称-Group
                Address = "127.0.0.1", //ip地址
                Port = 5200, //端口
                Tags=new string[] {"后台站点" },
                Check = new AgentServiceCheck()
                {
                    Interval = TimeSpan.FromSeconds(12),//多久检查一次心跳
                    HTTP = $"{domain}/Api/Health/Index",
                    Timeout=TimeSpan.FromSeconds(5),//超时时间
                    DeregisterCriticalServiceAfter=TimeSpan.FromSeconds(5) //取消注册时间
                }

            }) ;
        }
    }
}

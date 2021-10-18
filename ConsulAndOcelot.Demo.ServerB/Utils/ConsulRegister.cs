using Consul;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsulAndOcelot.Demo.ServerB.Utils
{
    /// <summary>
    /// Consul注册
    /// </summary>
    public static class ConsulRegister
    {
        //服务注册
        public static IApplicationBuilder UseConsul(this IApplicationBuilder app, IConfiguration configuration)
        {
            // 获取主机生命周期管理接口
            var lifetime = app.ApplicationServices.GetRequiredService<IHostApplicationLifetime>();

            ConsulClient client = new ConsulClient(c =>
            {
                c.Address = new Uri(configuration["Consul:consulAddress"]);
                c.Datacenter = "dc1";
            });
            string ip = configuration["ip"];
            string port = configuration["port"];
            string currentIp = configuration["Consul:currentIP"];
            string currentPort = configuration["Consul:currentPort"];

            ip = string.IsNullOrEmpty(ip) ? currentIp : ip; //当前程序的IP
            port = string.IsNullOrEmpty(port) ? currentPort : port; //当前程序的端口
            string serviceId = $"service:{ip}:{port}";//服务ID，一个服务是唯一的
            //服务注册
            client.Agent.ServiceRegister(new AgentServiceRegistration()
            {
                ID = serviceId, //唯一的
                Name = configuration["Consul:serviceName"], //组名称-Group
                Address = ip, //ip地址
                Port = int.Parse(port), //端口
                Tags = new string[] { "api站点" },
                Check = new AgentServiceCheck()
                {
                    Interval = TimeSpan.FromSeconds(10),//多久检查一次心跳
                    HTTP = $"http://{ip}:{port}/Health/Index",
                    Timeout = TimeSpan.FromSeconds(5),//超时时间
                    DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(5) //服务停止多久后注销服务
                }

            }).Wait();
            //应用程序终止时,注销服务
            lifetime.ApplicationStopping.Register(() =>
            {
                client.Agent.ServiceDeregister(serviceId).Wait();
            });
            return app;
        }
    }
}

using Polly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ConsulAndOcelot.Demo.ServerB.Utils
{
    public class OrderService
    {
        private Policy<string> _policy;
        public OrderService()
        {
            //重试
            Policy<string> retry = Policy<string>.Handle<Exception>()
              .WaitAndRetry(new TimeSpan[] { TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(10), TimeSpan.FromSeconds(15) });

            //降级
            Policy<string> fallback = Policy<string>
                 .Handle<Exception>() //异常故障
                 .Fallback(() =>
                 {
                    //降级回调
                    return "降级后的值";
                 });
            //Wrap：包裹。policyRetry在里面，policyFallback裹在外面。
            //如果里面出现了故障，则把故障抛出来给外面
            //_policy=Policy.Wrap(policy1, policy2, policy3, policy4, policy5);把更多一起封装。
            _policy = Policy.Wrap(fallback, retry); // fallback.Wrap<string>(retry);
        }
        public string CreateOrder()
        {
            //用polly执行
            return _policy.Execute(() =>
            {
                //业务逻辑 todo
                Console.WriteLine($"{DateTime.Now},开始处理业务");
                throw new Exception("233出错啦");
                Console.WriteLine("处理完成");
                return "成功啦";
            });
        }
    }
}

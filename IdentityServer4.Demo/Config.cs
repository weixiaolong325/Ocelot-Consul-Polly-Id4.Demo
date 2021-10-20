using IdentityServer4.Models;
using IdentityServer4.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer4.Demo
{
    /// <summary>
    /// 配置
    /// </summary>
    public class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
 new IdentityResource[]
 {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
 };
        /// <summary>
        /// 定义作用域
        /// </summary>
        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
            new ApiScope("gatewayScope"),
            new ApiScope("scope2")
            };

        public static IEnumerable<ApiResource> ApiResources =>
            new ApiResource[]
            {

                new ApiResource("server1","服务1")
                {
                    //4.x必须写
                    Scopes = { "gatewayScope" }
                },
            };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
            new Client
            {
                ClientId = "client_test",
                ClientName = "测试客户端",

                AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                ClientSecrets = { new Secret("secret_test".Sha256()) },

                AllowedScopes = { "gatewayScope" }
            },
            };

        /// <summary>
        /// 测试的账号和密码
        /// </summary>
        /// <returns></returns>
        public static List<TestUser> GetTestUsers()
        {
            return new List<TestUser>
    {
        new TestUser()
        {
             SubjectId = "1",
             Username = "test",
             Password = "123456"
        }
    };
        }
    }
}

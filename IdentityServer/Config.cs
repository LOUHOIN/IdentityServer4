using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer
{
    public static class Config
    {
        // 定义被保护的API范围（Scope）
        public static IEnumerable<ApiScope> ApiScopes => new[]
        {
            new ApiScope
            {
                Name = "api_1",
                DisplayName = "sample_api_1"
            },
        };
        // 定义客户端
        public static IEnumerable<Client> Clients => new[]
        {
            new Client
            {
                ClientId = "Client_1",
                ClientSecrets =
                {
                    // 一个ID可以有多个密钥
                    new Secret("Secret_1".Sha256()),
                    new Secret("Secret_2".Sha256())
                },
                //指定客户端凭据许可模式
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                //客户端允许的API范围,可以多组
                AllowedScopes ={"api_1"}
            },
        };
    }
}

// 客户端通过验证后可以获得访问令牌（access token），api范围会作为访问令牌中的信息，
// 当客户端携带访问令牌的时候就可以访问api资源，授权服务会验证携带的api范围是否正确
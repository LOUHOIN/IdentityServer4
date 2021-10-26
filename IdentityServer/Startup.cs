using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace IdentityServer
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // ע��MVC����
            services.AddControllersWithViews();
            // ע��Identity Server 4 �ķ���
            var builder = services.AddIdentityServer();
            builder.AddDeveloperSigningCredential();// ������Ӧ�õ�ʱ��Ϊ�������ṩһ����ʱ��Կ�����ļ���ʽ����
            // �������ڴ��У�֮��ĳ�DB
            builder.AddInMemoryApiScopes(Config.ApiScopes)
                   .AddInMemoryClients(Config.Clients)
                   .AddTestUsers(Config.GetUsers);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //MVC
            app.UseStaticFiles(); // ��̬��Դ�м��
            app.UseRouting(); // ע��·���м��

            app.UseIdentityServer();
            app.UseAuthorization(); //ע����Ȩ�м��

            // ע��˵��м��
            app.UseEndpoints(enptoints =>
            {
                enptoints.MapDefaultControllerRoute(); // Ĭ�Ͽ�����·��
            });
        }
    }
}

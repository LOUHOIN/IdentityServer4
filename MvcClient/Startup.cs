using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace MvcClient
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            // �ر������Ϣ����ӳ��,�Ѳ�������ӳ��������ռ䣬Ӱ����Ϣ����
            JwtSecurityTokenHandler.DefaultMapInboundClaims = false;
            // �����֤����
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = "Cookies"; // Ĭ����֤�������ƣ������Ʊ�����cookies��ͨ��cookies��������
                options.DefaultChallengeScheme = "oldc"; // ��ѯ������MVC��IDS����ʹ�õ�Э��
            })
                    .AddCookie("Cookies")
                    .AddOpenIdConnect("oidc", options =>
                    {
                        options.Authority = "https://localhost:5001";
                        options.ClientId = "Client_mvc";
                        options.ClientSecret = "Secret_mvc";
                        options.ResponseType = "code"; //��Ȩ������
                        options.SaveTokens = true; //���Ʊ��浽cookies
                    });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication(); // �����֤�м��
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}

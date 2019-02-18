using LabExam.DataSource;
using LabExam.IServices;
using LabExam.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace LabExam
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
            var connectionString = Configuration["ConnectionStrings:DefaultConnection"];
            services.AddDbContext<LabContext>(options => options.UseSqlServer(connectionString));

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => false;
                options.MinimumSameSitePolicy = SameSiteMode.None;
                options.HttpOnly = HttpOnlyPolicy.Always;
            });

            services.Configure<FormOptions>(x =>
            {
                x.ValueLengthLimit = 2147483647;
                x.MultipartBodyLengthLimit = 3221225472; //3G
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddHttpContextAccessor();
            services.TryAddSingleton<IActionContextAccessor, ActionContextAccessor>();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {                
                options.Cookie.HttpOnly = true;
                options.Cookie.Expiration = TimeSpan.FromDays(3);
                options.LoginPath = "/Account/Login";
                options.LogoutPath = "/Account/LoginOut";
            });

            services.AddDistributedMemoryCache();
            services.AddSession(); 

            /* 注册自己的服务 */
            services.AddTransient<IEncryptionDataService, EncryptionDataService>();
            services.AddTransient<ILoadConfigFileService, LoadConfigFileService>();
            services.AddTransient<IHttpContextAnalysisService, HttpContextAnalysisService>();
            
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseAuthentication();

            app.UseSession();
            app.UseStatusCodePagesWithReExecute("/error/{0}"); //用于状态错误的中间件

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}

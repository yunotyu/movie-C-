using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using movies.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.FileProviders;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace movies
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
            services.AddDbContext<MoviesContext>(options =>
            {
                //options.UseSqlServer(Configuration.GetConnectionString("connStr"));
            });

            services.AddControllersWithViews();
            //配置跨域
            //注意：.net Core 3.1版本  Cors配置不能同时启用  AllowAnyOrigin()  .AllowAnyMethod()  .AllowAnyHeader()  .AllowCredentials()
            //这里的AddPolicy的参数1是策略名，在后面要用到
            services.AddCors(options => options.AddPolicy("AllowSpecificOrigin",
                builder =>
                {
                    builder.WithMethods("GET", "POST", "HEAD", "PUT", "DELETE", "OPTIONS")
                .AllowAnyOrigin();

                }));

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
            }

           

            app.UseStaticFiles();
            //配置可以访问静态的图片文件
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(
                    //图片所在目录
                    Path.Combine(Directory.GetCurrentDirectory(), @"Static")), //用于定位资源的文件系统
                RequestPath = new PathString("/Static") //请求地址
            });

            app.UseRouting();

            app.UseAuthorization();

            //注意： app.UseCors("any"); 要写在app.UseAuthorization(); 后面，否则会报错。
            //这里要使用上面的策略名
            app.UseCors("AllowSpecificOrigin");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}

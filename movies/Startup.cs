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
            //���ÿ���
            //ע�⣺.net Core 3.1�汾  Cors���ò���ͬʱ����  AllowAnyOrigin()  .AllowAnyMethod()  .AllowAnyHeader()  .AllowCredentials()
            //�����AddPolicy�Ĳ���1�ǲ��������ں���Ҫ�õ�
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
            //���ÿ��Է��ʾ�̬��ͼƬ�ļ�
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(
                    //ͼƬ����Ŀ¼
                    Path.Combine(Directory.GetCurrentDirectory(), @"Static")), //���ڶ�λ��Դ���ļ�ϵͳ
                RequestPath = new PathString("/Static") //�����ַ
            });

            app.UseRouting();

            app.UseAuthorization();

            //ע�⣺ app.UseCors("any"); Ҫд��app.UseAuthorization(); ���棬����ᱨ��
            //����Ҫʹ������Ĳ�����
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

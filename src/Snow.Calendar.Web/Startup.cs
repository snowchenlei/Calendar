using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AspectCore.Configuration;
using AspectCore.Extensions.DependencyInjection;
using AspectCore.Injector;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.PlatformAbstractions;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Snow.Calendar.Web.Common;
using Snow.Calendar.Web.Interceptor;
using Snow.Calendar.Web.Model;
using Swashbuckle.AspNetCore.Swagger;

namespace Snow.Calendar.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration
            )
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<Resource>();
            services.AddTransient<ChineseCalendarInfo>();
            services.AddTransient<IDateHelper, DateHelper>();
            services.AddTransient<SolarTerm>();
            services.AddTransient<IBuildHtml, BuildHtml>();
            services.AddTransient<Constellation>();

            services.AddMemoryCache();

            services.AddMvc(options =>
                {
                    #region 输出缓存配置

                    options.CacheProfiles.Add("Default",
                        new CacheProfile()
                        {
                            Duration = 120
                        });
                    options.CacheProfiles.Add("Header",
                        new CacheProfile()
                        {
                            Duration = 120,
                            VaryByHeader = "User-Agent"
                        });
                    options.CacheProfiles.Add("Never",
                        new CacheProfile()
                        {
                            Location = ResponseCacheLocation.None,
                            NoStore = true
                        });

                    #endregion 输出缓存配置
                })
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    options.SerializerSettings.DateFormatString = "yyyy/MM/dd HH:mm:ss";
                }).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            #region Swagger

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Title = "万年历接口",
                    Version = "v1",
                    Contact = new Contact
                    {
                        Name = "Snow",
                        Email = "1007215202@qq.com",
                        Url = String.Empty
                    },
                    Description = "支持节日查询，年月查询，农历查询"
                });
                var basePath = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, basePath);
                c.IncludeXmlComments(xmlPath, true);
            });

            #endregion Swagger

            services.AddCors();
            services.AddSingleton<IFileProvider>(
                new PhysicalFileProvider(Directory.GetCurrentDirectory()));

            services.ConfigureDynamicProxy(config =>
            {
                config.Interceptors.AddTyped<CacheInterceptorAttribute>();
            });
            var container = services.ToServiceContainer();
            return container.Build();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
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

            app.UseCors(builder =>
                builder.WithOrigins("http://example.com"));

            //app.UseHttpsRedirection();

            #region Swagger

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Calendar API V1");
            });

            #endregion Swagger

            app.UseStaticFiles();
            // 自定义异常处理中间件
            app.UseMiddleware(typeof(ExceptionHandlerMiddleWare));

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
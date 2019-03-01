using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using ERPManager.Core.Model;
using ERPManager.Core.Settings;
using ERPManager.DataAccess.Abstract;
using ERPManager.DataAccess.Model;
using ERPManager.Service.Abstract;
using ERPManager.Service.Model;
using ERPManager.Web.MiddleWares;
using IdentityModel;
using IdentityServer4;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using HotelManager.Web.Configuration;

namespace HotelManager.Web
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

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton(Configuration.GetSection("IdentityServerSettings").Get<IdentityConfigurationOptions>());
            services.AddSingleton(Configuration.GetSection("SiteSettings").Get<SiteConfigurationOptions>());
            services.AddSingleton(Configuration.GetSection("ErpManagerSettings").Get<ErpManagerSettings>());
            services.Configure<ConfigurationOptions>(Configuration);
            services.AddTransient<IRepository, MongoRepository>();
            services.AddSingleton<ICacheService, CacheService>();
            services.AddSingleton<ILanguageService, LanguageService>();
            services.AddSingleton<IRoomService, RoomService>();


            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = "Cookies";
                options.DefaultChallengeScheme = "oidc";
            })
                .AddCookie("Cookies")
                .AddOpenIdConnect("oidc", options =>
                {
                    options.SignInScheme = "Cookies";
                    options.Authority = IdentityConfigurationOptions.Authority;
                    options.RequireHttpsMetadata = IdentityConfigurationOptions.RequireHttpsMetadata;
                    options.ClientId = "mvc";
                    options.ClientSecret = "secret";
                    options.SaveTokens = true;
                    options.GetClaimsFromUserInfoEndpoint = true;
                    options.Scope.Add("api1");
                    options.Scope.Add("profile");

                    options.ResponseType = "code id_token";
                    //options.ResponseType = "code id_token token";
                });

            services.AddSession();
            services.AddDistributedMemoryCache();

            services.AddMemoryCache();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
               .SetBasePath(env.ContentRootPath)
               .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/error");
            }

            app.UseAuthentication();
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseFileServer();
            app.UseNodeModules(env.ContentRootPath);
            app.UseSession();
            app.UseDeveloperExceptionPage();
            app.UseMvc(configureRoutes);
        }
        private void configureRoutes(IRouteBuilder routebuilder)
        {
            routebuilder.MapRoute("Default", "{Controller=Home}/{Action=Index}/{id?}");
        }
    }
}

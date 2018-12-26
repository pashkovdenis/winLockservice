using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommandBus;
using EncryptionService;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WinGuard.Domain.Interface;
using WinGuard.Domain.Model;
using WinGuard.Server.App.Services;
using WinGuardSession.Store;

namespace WinGuardServer
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddTransient<ISessionStore, InMemoryStore>();
            services.AddSingleton<IMessageBus, MessageBus>();
            services.AddTransient<IClientAuthService, ClientSessionService>(); 

            // 
            services.AddTransient<IEncryptionService, EncryptDecryptService>(e => new EncryptDecryptService(Keys.AesIV256,Keys.AesKey256));

            services.AddMemoryCache();
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
                app.UseExceptionHandler("/Error");
            }
            app.UseStaticFiles();
            app.UseCookiePolicy(); 
            app.UseMvcWithDefaultRoute();
        }
    }
}

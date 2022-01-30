using CoreApiClient.Clients;
using CoreApiClient.Interfaces;
using CoreApiClient.Utility;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace MyCards
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

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
            .AddCookie(options =>
            {
                options.LoginPath = "/account/google-login"; // Must be lowercase
            })
            .AddGoogle(options =>
            {
                options.ClientId = "1004139561983-jnk68700g5lr2qdfcl0bhlci4um78jeq.apps.googleusercontent.com";
                options.ClientSecret = "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";
            });

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IUserClient, UserClient>();
            services.AddTransient<SessionUtility, SessionUtility>();
            services.AddTransient<IDeckCardClient, DeckCardClient>();

            services.AddSession(options =>
            {
                options.Cookie.HttpOnly = true;
                options.Cookie.Name = ".Fiver.Session";
                options.IdleTimeout = TimeSpan.FromHours(1);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCookiePolicy();
            app.UseSession();
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

            app.UseAuthentication();
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

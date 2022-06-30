using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using projekt.Data;
using Microsoft.AspNetCore.Http;

namespace projekt
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
            services.AddAuthentication("CookieAuthentication")
             .AddCookie("CookieAuthentication", config =>
             {
                 config.Cookie.HttpOnly = true;
                 config.Cookie.SecurePolicy = CookieSecurePolicy.None;
                 config.Cookie.Name = "UserLoginCookie";
                 config.LoginPath = "/Login/Index";
                 config.AccessDeniedPath = "/Login/Index";
                 config.Cookie.SameSite = SameSiteMode.Strict;
             });

            services.AddAuthorization(options => {

                options.AddPolicy("Admin",
                    policy => policy.RequireClaim("Admin"));

                options.AddPolicy("AdminOrStaff",
                    policy => policy.RequireClaim("AdminOrStaff"));

                options.AddPolicy("Client",
                    policy => policy.RequireClaim("Client"));
            });

            services.AddRazorPages();

            services.AddDbContext<ProjectContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("ProjectContext")));

            services.Add(new ServiceDescriptor(typeof(UserDB), new UserDB(Configuration)));

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
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseCookiePolicy();
            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}

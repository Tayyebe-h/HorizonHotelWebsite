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
using HorizonHotelWebsite.Data;
using Microsoft.EntityFrameworkCore;
using HorizonHotelWebsite.Models.Repositories;
using Microsoft.AspNetCore.Identity;
using HorizonHotelWebsite.Models.Entities.user;
using HorizonHotelWebsite.Helpers;

namespace HorizonHotelWebsite
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
                       

            services.AddScoped<IAdminBookingRepository, AdminBookingRepository>();
            services.AddScoped<IAdminRoomRepo, AdminRoomRepo>();
            services.AddScoped<IAdminUserRepository, AdminUserRepository>();
            services.AddScoped<ICustomerBookingRepository, CustomerBookingRepository>();
            services.AddScoped<IPayment, PaymentRepo>();


            services.AddDbContext<DataBaseContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("DataBaseContext")));

            services.AddControllersWithViews();

            services.AddIdentity<User,Role>()
               .AddEntityFrameworkStores<DataBaseContext>()
               .AddDefaultTokenProviders();

            services.AddScoped<IUserClaimsPrincipalFactory<User>, AddMyClaim>();
            services.AddAuthorization(options =>
            {
                options.AddPolicy("UserRole", policy =>
                {
                    policy.RequireClaim("UserRole","Admin","Operator");
                });
            });
            services.AddRazorPages();
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

            app.UseAuthentication();

            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();

            });


        }
    }
}

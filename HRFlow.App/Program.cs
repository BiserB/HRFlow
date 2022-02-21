using HRFlow.App.Infrastructure;
using HRFlow.Common.MappingProfiles;
using HRFlow.Data;
using HRFlow.Services;
using HRFlow.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace HRFlow.App
{
    public class Program
    {
        static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

            builder.Services.AddDbContext<HRFlowDbContext>(b =>
            {
                b.UseNpgsql(builder.Configuration.GetConnectionString("HRFlowDb"));
            });

            builder.Services.AddScoped<IEmployeeService, EmployeeService>();

            //builder.Services.AddAutoMapper(options => options.AddProfile<EmployeeMappingProfile>());

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.Seed();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}

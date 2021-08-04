﻿using System;
using Final_Project_Car_Wash.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(Final_Project_Car_Wash.Areas.Identity.IdentityHostingStartup))]
namespace Final_Project_Car_Wash.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<Final_Project_Car_WashContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("Final_Project_Car_WashContextConnection")));

                services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<Final_Project_Car_WashContext>();
            });
        }
    }
}
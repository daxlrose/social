﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Social.Dal;

namespace Social.Api.Registers
{
    public class DbRegister : IWebApplicationBuilderRegister
    {
        public void RegisterServices(WebApplicationBuilder builder)
        {
            var cs = builder.Configuration.GetConnectionString("Default");

            builder.Services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlServer(cs);
            });

            builder.Services.AddIdentityCore<IdentityUser>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 5;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
            })
            .AddEntityFrameworkStores<DataContext>();
        }
    }
}

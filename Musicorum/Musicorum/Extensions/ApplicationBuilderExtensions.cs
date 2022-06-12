﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Musicorum.Data;
using Musicorum.Data.Entities;
using Musicorum.Web.Infrastructure;
using System.Threading.Tasks;

namespace Musicorum.Web.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseDatabaseMigrations(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                serviceScope.ServiceProvider.GetService<MusicorumDbContext>().Database.Migrate();

                var userManager = serviceScope.ServiceProvider.GetService<UserManager<User>>();
                
                var roleManager = serviceScope.ServiceProvider.GetService<RoleManager<IdentityRole>>();
                
                Task.Run(async () =>
                {
                    var roleName = GlobalConstants.AdminRole;
                
                    var roleExits = await roleManager.RoleExistsAsync(roleName);
                
                    if (!roleExits)
                    {
                        var result = await roleManager.CreateAsync(new IdentityRole
                        {
                            Name = roleName
                        });
                    }
                
                    var adminUser = await userManager.FindByNameAsync(roleName);
                
                    if (adminUser == null)
                    {
                        adminUser = new User
                        {
                            FirstName = "Administrator",
                            LastName = "Administrator",
                            Age = 23,
                            Email = "admin@social.com",
                            UserName = roleName
                        };
                
                        await userManager.CreateAsync(adminUser, "admin123");
                
                        await userManager.AddToRoleAsync(adminUser, roleName);
                    }
                })
                .GetAwaiter()
                .GetResult();
            }
            return app;
        }
    }
}
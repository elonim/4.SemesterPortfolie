using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Platformservice.Models;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Platformservice.Data
{
    public static class PrepDb
    {
        public static void Preppopulation(IApplicationBuilder app, bool IsProduction)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                SeedDate(serviceScope.ServiceProvider.GetService<AppDbContext>(), IsProduction);
            }
        }


        private static void SeedDate(AppDbContext context, bool IsProduction)
        {
            if(IsProduction)
            {
                Console.WriteLine("--> Attempting to apply migrations!");
                try{
                context.Database.Migrate();
                }
                catch(Exception ex)
                {
                    Console.WriteLine($"--> Could not run migrations: {ex.Message}");
                }
            }

            if (!context.Platforms.Any())
            {
                Console.WriteLine("--> Seeding Data!");

                context.AddRange(
                    new Platform { Name = "Dot Net", Publisher = "MicroSoft", Cost = 0 },
                    new Platform { Name = "SQL Server Express", Publisher = "Microsoft", Cost = 0 },
                    new Platform { Name = "Kubernetes", Publisher = "Clout Native Computing", Cost = 0 }
                    );

                context.SaveChanges();
                return;
            }
            else
            {
                Console.WriteLine("--> We already have data!");
            }
        }
    }
}

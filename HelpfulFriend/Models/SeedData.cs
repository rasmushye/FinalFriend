using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using HelpfulFriend.Data;
using System;
using System.Linq;

namespace HelpfulFriend.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                // Look for any movies.
                if (context.JobOffers.Any())
                {
                    return;   // DB has been seeded
                }

                context.JobOffers.AddRange(
                     new JobOffers
                     {
                         JobTitle = "Lawnmoving",
                         JobDate = DateTime.Parse("1989-1-11"),
                         Category = "Gardening",
                         Price = 7.99
                     },

                     new JobOffers
                     {
                         JobTitle = "Wheelshift",
                         JobDate = DateTime.Parse("1984-3-13"),
                         Category = "Auto",
                         Price = 8.99
                     },

                     new JobOffers
                     {
                         JobTitle = "TV setup",
                         JobDate = DateTime.Parse("1986-2-23"),
                         Category = "IT",
                         Price = 9.99
                     },

                   new JobOffers
                   {
                       JobTitle = "Learn guitar",
                       JobDate = DateTime.Parse("1959-4-15"),
                       Category = "Music",
                       Price = 3.99
                   }
                );
                context.SaveChanges();
            }
        }
    }
}

using FriendsForever_App.Models;
using FriendsForever_App.Security;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FriendsForever_App.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Country> CountryMaster { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Seed();
            modelBuilder.Entity<ApplicationUser>()
                .Property(e => e.FullName)
                .HasComputedColumnSql("[LastName] + ', ' + [FirstName]");
            base.OnModelCreating(modelBuilder);
        }
    }
}

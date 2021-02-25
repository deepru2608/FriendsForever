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
        public DbSet<LogForLogin> LogTableForLogin { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<QualityParameterTable> QualityParameters { get; set; }
        public DbSet<UserInterest> UserInterestMaster { get; set; }
        public DbSet<Post> PostMaster { get; set; }
        public DbSet<Likes> LikesMaster { get; set; }
        public DbSet<Comments> CommentsMaster { get; set; }
        public DbSet<PostImages> PostImagesMaster { get; set; }
        public DbSet<Friends> FriendsMaster { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Seed();
            modelBuilder.Entity<ApplicationUser>()
                .Property(e => e.FullName)
                .HasComputedColumnSql("[FirstName] + ' ' + [LastName]");
            modelBuilder.Entity<Friends>()
                .Property(e => e.Status)
                .HasDefaultValue<string>("Pending");
            base.OnModelCreating(modelBuilder);
        }
    }
}

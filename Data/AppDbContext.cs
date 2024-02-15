using GraphQLTest.CoreLayer.Entities;
using GraphQLTest.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;

namespace GraphQLTest.Data
{
 

    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Post> Posts { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

 
        // Saves changes made to the database.
        public override int SaveChanges()
        {
            try
            {
                ChangeTracker.DetectChanges();
                return base.SaveChanges();
            }
            catch (Exception ex)
            {

                throw ;
            }
        }
    }
}



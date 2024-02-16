using System;
using GraphQLTest.Data;
using GraphQLTest.DataAccess;
using GraphQLTest.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using Microsoft.Extensions.Configuration;

namespace TestProjectUnit1
{
    public class UnitTest1
    {
        [Fact]
        public void TestDbContextScoping()
        {
            var connectionString = "Host=localhost;Database=MYN_Test_DB;Username=postgres;Password=Server.123;Port=5432";
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddDbContextFactory<AppDbContext>(options =>
            {
                options.UseNpgsql(connectionString);
            });

            serviceCollection.AddScoped<IDataAccessProvider, DataAccessProvider>();

            using (var serviceProvider = serviceCollection.BuildServiceProvider())
            {
                using (var scope = serviceProvider.CreateScope())
                {
                    var dataAccessProvider = scope.ServiceProvider.GetRequiredService<IDataAccessProvider>();

                    // Call the methods on DataAccessProvider
                    var user1 = dataAccessProvider.AddUser(new User { /*...user data...*/ });
                    var user2 = dataAccessProvider.GetUserById(user1.Id);

                    // Assert that the database connections are different for each method call
                    Assert.NotEqual(user1, user2);
                }
            }
        }
    }
}
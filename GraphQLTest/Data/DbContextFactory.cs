using Microsoft.EntityFrameworkCore;

namespace GraphQLTest.Data
{
    public class DbContextFactory : IDbContextFactory<AppDbContext>
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public DbContextFactory(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public AppDbContext CreateDbContext()
        {
            var scope = _serviceScopeFactory.CreateScope();
            var serviceProvider = scope.ServiceProvider;

            // Retrieve an instance of AppDbContext from the service provider
            var dbContext = serviceProvider.GetRequiredService<AppDbContext>();

            return dbContext;
        }
    }
}

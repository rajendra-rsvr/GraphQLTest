using GraphQLTest.Data;
using GraphQLTest.Users;
using HotChocolate.Execution;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Testcontainers.PostgreSql;
using GraphQLTest.IServices;

namespace TestProjectUnit1
{
    public class UserSchemaTest
    {
        private readonly PostgreSqlContainer _postgres = new PostgreSqlBuilder()
           .WithImage("postgres:15-alpine")
           .Build();
        private readonly IRequestExecutorResolver _resolver;

        public UserSchemaTest()
        {
            var PostgresConnectionString = "Host=localhost;Database=MYN_Test_DB;Username=postgres;Password=Server.123;Port=5432";

            var services = new ServiceCollection();
            services
                .AddDbContext<AppDbContext>(options => options.UseNpgsql(PostgresConnectionString), ServiceLifetime.Scoped)
                .AddGraphQL()
                .AddQueryType()
                .AddMutationType()
                .AddTypeExtension<Query>()
                .AddTypeExtension<Mutation>();

            var buildServiceProvider = services.BuildServiceProvider();
            _resolver = buildServiceProvider.GetRequiredService<IRequestExecutorResolver>();
        }
        
        [Fact]
        public async Task TestGetUsersQuery()
        {
            IRequestExecutor executor = await _resolver.GetRequestExecutorAsync();

            var request = QueryRequestBuilder.New()
                .SetQuery("{ allUsers {nodes{ id   firstName   lastName  email  address  }  } }")
                .Create();

            IExecutionResult result = await executor.ExecuteAsync(request);
            Assert.NotNull(result); // Ensure data is returned
        }


        public Task InitializeAsync()
        {
            return _postgres.StartAsync();
        }

        public Task DisposeAsync()
        {
            return _postgres.DisposeAsync().AsTask();
        }
    }
}

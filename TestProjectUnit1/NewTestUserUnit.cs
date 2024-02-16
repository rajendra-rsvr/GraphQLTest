using GraphQLTest.Data;
using GraphQLTest.Users;
using HotChocolate.Execution;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Xunit;
using GraphQLTest.IServices;
using TestContainers.Container.Abstractions.Hosting;
using TestContainers.Container.Database.PostgreSql;

namespace TestProjectUnit1
{
    public class NewTestUserUnit : IAsyncLifetime
    {
        private readonly PostgreSqlContainer _postgres = new ContainerBuilder<PostgreSqlContainer>()
            .Build();

        private IRequestExecutor _executor;

        public async Task InitializeAsync()
        {
            await _postgres.StartAsync();

            var postgresPort = _postgres.GetMappedPort(5432);

            var postgresConnectionString = $"Host=localhost;Database=MYN_Test_DB;Username=postgres;Password=Server.123;Port={postgresPort}";

            var services = new ServiceCollection();
            services
                .AddDbContext<AppDbContext>(options => options.UseNpgsql(postgresConnectionString), ServiceLifetime.Scoped)
                .AddGraphQL()
                .AddQueryType()
                .AddMutationType()
                .AddTypeExtension<Query>()
                .AddTypeExtension<Mutation>();

            var serviceProvider = services.BuildServiceProvider();
            _executor = await serviceProvider.GetRequiredService<IRequestExecutorResolver>().GetRequestExecutorAsync();
        }

        [Fact]
        public async Task TestGetUsersQuery()
        {
            // Ensure GraphQL schema is initialized
            Assert.NotNull(_executor);

            var request = QueryRequestBuilder.New()
                .SetQuery("{ allUsers {nodes{ id   firstName   lastName  email  address  }  } }")
                .Create();

            IExecutionResult result = await _executor.ExecuteAsync(request);

            // Ensure data is returned
            Assert.NotNull(result.ContextData);

            // Validate the structure of the returned data
            Assert.IsType<Dictionary<string, object>>(result.ContextData);
            var allUsers = result.ContextData["allUsers"] as Dictionary<string, object>;
            Assert.NotNull(allUsers);

            var nodes = allUsers["nodes"] as IEnumerable<object>;
            Assert.NotNull(nodes);

            foreach (var node in nodes)
            {
                var user = node as Dictionary<string, object>;
                Assert.NotNull(user);

                // Add specific assertions for the content of user data
                Assert.NotNull(user["id"]);
                Assert.NotNull(user["firstName"]);
                Assert.NotNull(user["lastName"]);
                Assert.NotNull(user["email"]);
                Assert.NotNull(user["address"]);
            }
        }

        public async Task DisposeAsync()
        {
            await _postgres.StopAsync();
            // Additional cleanup or resource release if needed
        }

    }
}

using GraphQLTest.CoreLayer.Interfaces;
using GraphQLTest.Data;
using GraphQLTest.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace GraphQLTest
{
    public class Startup
    {

        public void ConfigureServices(IServiceCollection services)
        {

            var builder = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            IConfiguration configuration = builder.Build();

            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(configuration["ConnectionString"]));
            services.AddScoped<IPostRepository, PostRepository>();
        }

    }
}

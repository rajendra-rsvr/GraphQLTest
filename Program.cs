using GraphQLTest.Data;
using GraphQLTest.DataAccess;
using GraphQLTest.Users;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var sqlConnectionString =  builder.Configuration["ConnectionString"];

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowOrigin", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(sqlConnectionString), ServiceLifetime.Singleton);

builder.Services.AddTransient<IDataAccessProvider, DataAccessProvider>();

builder.Services.AddGraphQLServer().AddQueryType<GraphQLTest.IServices.Query>()
    .AddMutationType<UserMutations>(); builder.Services.AddScoped<IDataAccessProvider, DataAccessProvider>();


var app = builder.Build();
app.UseRouting();

// Use the CORS policy
app.UseCors("AllowOrigin");
app.UseEndpoints(endpoints =>
{
    endpoints.MapGraphQL();
});

app.MapGet("/", () => "GraphQL");
//app.MapGraphQL();

app.Run();
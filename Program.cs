using GraphQLTest.Data;
using GraphQLTest.DataAccess;
using GraphQLTest.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

var ConnectionString =  builder.Configuration["ConnectionString"];

// Configuring CORS (Cross-Origin Resource Sharing) for the application.
builder.Services.AddCors(options =>
{
    // Defining a CORS policy named "AllowOrigin".
    options.AddPolicy("AllowOrigin", builder =>
    {
        // Allowing requests from any origin.
        builder.AllowAnyOrigin()
               // Allowing requests with any HTTP method (GET, POST, etc.).
               .AllowAnyMethod()
               // Allowing requests with any headers.
               .AllowAnyHeader();
    });
});


// Adding a DbContext to the dependency injection container.
builder.Services.AddDbContextFactory<AppDbContext>(options =>
{
    options.UseNpgsql(ConnectionString);
}, ServiceLifetime.Singleton);

// Adding a transient registration for IDataAccessProvider and its implementation DataAccessProvider to the dependency injection container.
builder.Services.AddScoped<IDataAccessProvider, DataAccessProvider>();


// Adding a GraphQL server to the dependency injection container.
builder.Services.AddGraphQLServer()
    .AddQueryType<GraphQLTest.IServices.Query>()
    .AddMutationType<UserMutations>();
builder.Services.AddScoped<IDataAccessProvider, DataAccessProvider>();

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

// Running the application.
app.Run();
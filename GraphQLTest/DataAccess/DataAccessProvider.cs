using GraphQLTest.Data;
using GraphQLTest.Models;
using GraphQLTest.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.DependencyInjection;
using System;
using static System.Formats.Asn1.AsnWriter;

namespace GraphQLTest.DataAccess
{
    public class DataAccessProvider : IDataAccessProvider
    {
        private readonly IServiceScopeFactory _contextFactory;

        public DataAccessProvider(IServiceScopeFactory serviceScopeFactory)
        {
            _contextFactory = serviceScopeFactory;
        }

        // Retrieves a list of all users from the system.
        public List<User> GetAllUsers()
        {
            try
            {
                using(var scope = _contextFactory.CreateScope())
        {
                    var contextFactory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<AppDbContext>>();

                    using (var context = contextFactory.CreateDbContext())
                    {
                        var list = context.Users.ToList();
                        return list;
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }
           
        }

        // Retrieves a user from the system based on the specified ID.
        public User GetUserById(int id)
        {
            try
            {
                using (var scope = _contextFactory.CreateScope())
                {
                    var contextFactory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<AppDbContext>>();

                    using (var context = contextFactory.CreateDbContext())
                    {
                        return context.Users.FirstOrDefault(t => t.Id == id);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
         
        }

        public IQueryable<User> GetUsers()
        {

            using (var scope = _contextFactory.CreateScope())
            {
                var contextFactory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<AppDbContext>>();

                using (var context = contextFactory.CreateDbContext())
                {
                    var list = context.Users.ToList();
                    return list.AsQueryable();
                }
            }
        }


        // Retrieves an IQueryable collection of users from the system.
        public User AddUser(User user)
        {
            try
            {
                using (var scope = _contextFactory.CreateScope())
                {
                    var contextFactory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<AppDbContext>>();

                    using (var context = contextFactory.CreateDbContext())
                    {
                        context.Users.Add(user);
                        context.SaveChanges();

                        // Get the last inserted user
                        var lastUser = context.Users.OrderByDescending(u => u.Id).FirstOrDefault();
                        return lastUser;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new GraphQLException("An error occurred while adding the user.", ex);
            }
        }

        // Updates the information of an existing user in the system.
        public User UpdateUser(User user)
        {
            try
            {
                using (var scope = _contextFactory.CreateScope())
                {
                    var contextFactory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<AppDbContext>>();

                    using (var context = contextFactory.CreateDbContext())
                    {
                        context.Users.Update(user);
                        context.SaveChanges();
                        return context.Users.FirstOrDefault(t => t.Id == user.Id);
                    }
                }
            }
            catch (Exception ex)
            {
                return user;
            }
        }

        // Deletes a user with the specified ID from the system.
        public bool DeleteUser(int id)
        {
            try
            {
                using (var scope = _contextFactory.CreateScope())
                {
                    var contextFactory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<AppDbContext>>();

                    using (var context = contextFactory.CreateDbContext())
                    {
                        var entity = context.Users.FirstOrDefault(t => t.Id == id);
                        context.Users.Remove(entity);
                        context.SaveChanges();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public void Dispose()
        {
            using (var scope = _contextFactory.CreateScope())
            {
                var contextFactory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<AppDbContext>>();

                using (var context = contextFactory.CreateDbContext())
                {
                    context.Dispose();
                }
            }
        }
    }
}

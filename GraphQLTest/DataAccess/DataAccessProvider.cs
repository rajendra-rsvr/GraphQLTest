using GraphQLTest.Data;
using GraphQLTest.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GraphQLTest.DataAccess
{
    public class DataAccessProvider : IDataAccessProvider
    {
        private readonly IDbContextFactory<AppDbContext> _contextFactory;

        public DataAccessProvider(IDbContextFactory<AppDbContext> contextFactory)
        {
            _contextFactory = contextFactory ?? throw new ArgumentNullException(nameof(contextFactory));
        }

        public List<User> GetAllUsers()
        {
            try
            {
                using (var context = _contextFactory.CreateDbContext())
                {
                    var list = context.Users.ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public User GetUserById(int id)
        {
            try
            {
                using (var context = _contextFactory.CreateDbContext())
                {
                    return context.Users.FirstOrDefault(t => t.Id == id);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IQueryable<User> GetUsers()
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                var list = context.Users.ToList();
                return list.AsQueryable();
            }
        }

        public User AddUser(User user)
        {
            try
            {
                using (var context = _contextFactory.CreateDbContext())
                {
                    context.Users.Add(user);
                    context.SaveChanges();

                    var lastUser = context.Users.OrderByDescending(u => u.Id).FirstOrDefault();
                    return lastUser;
                }
            }
            catch (Exception ex)
            {
                throw new GraphQLException("An error occurred while adding the user.", ex);
            }
        }

        public User UpdateUser(User user)
        {
            try
            {
                using (var context = _contextFactory.CreateDbContext())
                {
                    context.Users.Update(user);
                    context.SaveChanges();
                    return context.Users.FirstOrDefault(t => t.Id == user.Id);
                }
            }
            catch (Exception ex)
            {
                return user;
            }
        }

        public bool DeleteUser(int id)
        {
            try
            {
                using (var context = _contextFactory.CreateDbContext())
                {
                    var entity = context.Users.FirstOrDefault(t => t.Id == id);
                    if (entity != null)
                    {
                        context.Users.Remove(entity);
                        context.SaveChanges();
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public void Dispose()
        {
            // No need to manually dispose, as the scoped DbContext will be handled by the DI container.
        }
    }
}

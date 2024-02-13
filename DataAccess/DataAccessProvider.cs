using GraphQLTest.Data;
using GraphQLTest.Models;
using GraphQLTest.Users;
using Microsoft.EntityFrameworkCore;

namespace GraphQLTest.DataAccess
{
    public class DataAccessProvider : IDataAccessProvider
    {
        //private readonly AppDbContext _context;
        private readonly IDbContextFactory<AppDbContext> _contextFactory;

        //public DataAccessProvider(AppDbContext context)
        //{
        //    _context = context;
        //}

        public DataAccessProvider(IDbContextFactory<AppDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        // Retrieves a list of all users from the system.
        public List<User> GetAllUsers()
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                var list = context.Users.ToList();
                return list;
            }
           
        }

        // Retrieves a user from the system based on the specified ID.
        public User GetUserById(int id)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                return context.Users.FirstOrDefault(t => t.Id == id);
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


        // Retrieves an IQueryable collection of users from the system.
        public User AddUser(User user)
        {
            try
            {
                using (var context = _contextFactory.CreateDbContext())
                {
                    context.Users.Add(user);
                    context.SaveChanges();

                    // Get the last inserted user
                    var lastUser = context.Users.OrderByDescending(u => u.Id).FirstOrDefault();
                    return lastUser;
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

        // Deletes a user with the specified ID from the system.
        public bool DeleteUser(int id)
        {
            try
            {
                using (var context = _contextFactory.CreateDbContext())
                {
                    var entity = context.Users.FirstOrDefault(t => t.Id == id);
                    context.Users.Remove(entity);
                    context.SaveChanges();
                    return true;

                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}

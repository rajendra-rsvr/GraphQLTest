using GraphQLTest.Data;
using GraphQLTest.DataAccess;
using GraphQLTest.Models;
using System.Diagnostics.Metrics;
using System.Net;

namespace GraphQLTest.IServices
{
    public class Mutation
    {
        //private readonly AppDbContext _context;
        private readonly IDataAccessProvider _IDataAccessProvider;
        public Mutation(IDataAccessProvider IDataAccessProvider)
        {
            _IDataAccessProvider = IDataAccessProvider;
        }

        // Adds a new user based on the provided information.
        public User AddUser(NewUser input)
        {
            User user = new User();
            user.FirstName = input.FirstName;
            user.LastName = input.LastName;
            user.Address = input.Address;
            return _IDataAccessProvider.AddUser(user);
        }

        // Updates a user with the specified ID and optional new information.
        public User UpdateUser(int id, string? firstName, string? lastName, string? email, string? address)
        {
            User users = new User();
            if (id <= 0)
            {
                return users;
            }

            User user = _IDataAccessProvider.GetUserById(id);

            if (user == null)
            {
                return users;
            }

            if (firstName != null)
            {
                user.FirstName = firstName;
            }
            if (lastName != null)
            {
                user.LastName = lastName;
            }
            if (email != null)
            {
                user.Email = email;
            }
            if (address != null)
            {
                user.Address = address;
            }
            return _IDataAccessProvider.UpdateUser(user);
        }
        
        // Deletes a user with the specified ID from the system.
        public bool DeleteUser(int id)
        {
            return _IDataAccessProvider.DeleteUser(id);
        }
    }
}

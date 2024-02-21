using GraphQLTest.Models;
using GraphQLTest.DataAccess;
using System.Collections.Generic;

namespace GraphQLTest.IServices
{
    public class Query
    {
        private readonly IDataAccessProvider _dataAccessProvider;

        public Query(IDataAccessProvider dataAccessProvider)
        {
            _dataAccessProvider = dataAccessProvider;
        }

        // Retrieves a list of all users from the system.
        public List<User> GetAllUsers()
        {
            return _dataAccessProvider.GetAllUsers();
        }

        // Retrieves a user from the system based on the specified ID.
        public User GetUserById(int id)
        {
            return _dataAccessProvider.GetUserById(id);
        }
    }
}

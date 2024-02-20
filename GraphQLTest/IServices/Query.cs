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
            try
            {
                return _dataAccessProvider.GetAllUsers();
            }
            catch (Exception ex)
            {
                // Log or handle the exception appropriately
                throw;
            }
        }

        // Retrieves a user from the system based on the specified ID.
        public User GetUserById(int id)
        {
            try
            {
                return _dataAccessProvider.GetUserById(id);
            }
            catch (Exception ex)
            {
                // Log or handle the exception appropriately
                throw;
            }
        }
    }
}

using GraphQLTest.Data;
using GraphQLTest.DataAccess;
using GraphQLTest.Models;
using System.Diagnostics.Metrics;
using System.Net;

namespace GraphQLTest.IServices
{
    public class Query
    {
        private readonly IDataAccessProvider _IDataAccessProvider;
        public Query(IDataAccessProvider IDataAccessProvider)
        {
            _IDataAccessProvider = IDataAccessProvider;
        }

        public List<User> GetAllUsers()
        {
            return _IDataAccessProvider.GetAllUsers();
        }

        public User GetUserById([ID]int id)
        {
            return _IDataAccessProvider.GetUserById(id);
        }

    }
}

using GraphQLTest.Models;

namespace GraphQLTest.DataAccess
{
    public interface IDataAccessProvider
    {
        List<User> GetAllUsers();
        IQueryable<User> GetUsers();
        User GetUserById(int id);
        User AddUser(User user);
        User UpdateUser(User user);
        bool DeleteUser(int id);
    }
}

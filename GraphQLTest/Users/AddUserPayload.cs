using GraphQLTest.Models;

namespace GraphQLTest.Users
{
    public class AddUserPayload
    {

        public User User { get; }

        public AddUserPayload(User user)
        {
            User = user;
        }
    }
}
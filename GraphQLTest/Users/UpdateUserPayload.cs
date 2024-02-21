using GraphQLTest.Models;

namespace GraphQLTest.Users
{
    public class UpdateUserPayload
    {

        public User User { get; }

        public UpdateUserPayload(User user)
        {
            User = user;
        }
    }
}
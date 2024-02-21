using GraphQLTest.DataAccess;
using GraphQLTest.Models;

namespace GraphQLTest.Users
{
    public class UserMutations
    {
        //private readonly AppDbContext _context;
        private readonly IDataAccessProvider _IDataAccessProvider;
        public UserMutations(IDataAccessProvider IDataAccessProvider)
        {
            _IDataAccessProvider = IDataAccessProvider;
        }
        public async Task<AddUserPayload> AddUser(AddUserInput input)
        {
            var user = new User
            {
                FirstName = input.FirstName,
                LastName = input.LastName,
                Email = input.Email,
                Address = input.Address,
            };


            _IDataAccessProvider.AddUser(user);

            return new AddUserPayload(user);
        }

        public async Task<UpdateUserPayload> UpdateUser(int id, UpdateUserInput input)
        {
            var existingUser = _IDataAccessProvider.GetUserById(id);

            if (existingUser == null)
            {
                // Handle the case where the user with the specified Id is not found
                return null;
            }

            if (input.FirstName != null)
            {
                existingUser.FirstName = input.FirstName;
            }
            if (input.LastName != null)
            {
                existingUser.LastName = input.LastName;
            }
            if (input.Email != null)
            {
                existingUser.Email = input.Email;
            }
            if (input.Address != null)
            {
                existingUser.Address = input.Address;
            }

            _IDataAccessProvider.UpdateUser(existingUser);

            return new UpdateUserPayload(existingUser);
        }
    }
}

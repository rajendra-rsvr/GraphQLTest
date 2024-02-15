using GraphQLTest.CoreLayer.Entities;
using GraphQLTest.CoreLayer.Interfaces;

namespace GraphQLTest.CoreLayer.Services
{
    public class PostService
    {
        private readonly IPostRepository _postRepository;

        public PostService(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public Post GetPostById(int id)
        {
            return _postRepository.GetById(id);
        }

        // Other methods that use _postRepository
    }

}

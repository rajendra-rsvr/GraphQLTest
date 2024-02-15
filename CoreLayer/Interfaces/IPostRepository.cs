using GraphQLTest.CoreLayer.Entities;
using System.Collections.Generic;

namespace GraphQLTest.CoreLayer.Interfaces
{
    public interface IPostRepository
    {
        Post GetById(int id);
        IEnumerable<Post> GetAll();
        void Add(Post post);
        void Update(Post post);
        void Delete(int id);
    }

}

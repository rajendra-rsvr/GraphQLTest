using GraphQLTest.Data;
using GraphQLTest.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GraphQLTest.DataAccess
{
    public class DataAccessProvider : IDataAccessProvider
    {
        private readonly AppDbContext _context;

        public DataAccessProvider(AppDbContext context)
        {
            _context = context;
        }

        public List<User> GetAllUsers()
        {
            var list = _context.Users.ToList();
            return list;
        }

        public User GetUserById(int id)
        {
            return _context.Users.FirstOrDefault(t => t.Id == id);
        }

        public IQueryable<User> GetUsers()
        {
            var list = _context.Users.ToList();
            return list.AsQueryable();
        }

        public User AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            var lastUser = _context.Users.OrderByDescending(u => u.Id).FirstOrDefault();
            return lastUser;
        }

        public User UpdateUser(User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
            return _context.Users.FirstOrDefault(t => t.Id == user.Id);
        }

        public bool DeleteUser(int id)
        {
            var entity = _context.Users.FirstOrDefault(t => t.Id == id);
            if (entity != null)
            {
                _context.Users.Remove(entity);
                _context.SaveChanges();
                return true;

            }
            return false;
        }
    }
}

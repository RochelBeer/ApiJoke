using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiJokes63.Data
{
    public class Repository
    {
        private readonly string _connectionString;
        public Repository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public void AddUser(User user, string password)
        {
            var hash = BCrypt.Net.BCrypt.HashPassword(password);
            user.PasswordHash = hash;
            using var context = new JokesDbContext(_connectionString);
            context.Users.Add(user);
            context.SaveChanges();
        }
        public User Login(string email, string password)
        {
            using var context = new JokesDbContext(_connectionString);
            var user = GetByEmail(email);
            if (user == null)
            {
                return null;
            }
            var isValidPassword = BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);
            if (!isValidPassword)
            {
                return null;
            }
            return user;
        }
        public User GetByEmail(string email)
        {
            using var context = new JokesDbContext(_connectionString);
            return context.Users.FirstOrDefault(u => u.Email == email);
        }
    }
}

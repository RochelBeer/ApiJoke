using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiJokes63.Data
{
    public class JokesRepository
    {
        private readonly string _connectionString;
        public JokesRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public Joke AddJoke(Joke joke)
        {
            using var context = new JokesDbContext(_connectionString);
            if (!context.Jokes.Any(j => j.JokeId == joke.JokeId))
            {
                context.Jokes.Add(joke);
                context.SaveChanges();
            }
            return context.Jokes.Include(j => j.UsersJokes).FirstOrDefault(j => j.JokeId == joke.JokeId);
        }
        public List<Joke> ViewJokes()
        {
            using var context = new JokesDbContext(_connectionString);
            return context.Jokes.Include(j => j.UsersJokes).ToList();
        }
        public void AddLike(UsersJokes usersJokes)
        {
            using var context = new JokesDbContext(_connectionString);
            var userLike = context.UsersJokes.FirstOrDefault(u => u.UserId == usersJokes.UserId && u.JokeId == usersJokes.JokeId);
            if (userLike == null)
            {
                context.UsersJokes.Add(usersJokes);
            }
            else
            {
                userLike.Liked = usersJokes.Liked;
            }

            context.SaveChanges();
        }

        public UsersJokes GetLike(int userId, int jokeId)
        {
            using var context = new JokesDbContext(_connectionString);
            return context.UsersJokes.FirstOrDefault(u => u.UserId == userId && u.JokeId == jokeId);

        }
        public Joke GetWithLike(int jokeId)
        {
            using var context = new JokesDbContext(_connectionString);
            return context.Jokes.Include(u => u.UsersJokes).FirstOrDefault(j => j.Id == jokeId);
        }

    }
}

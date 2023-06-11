using ApiJokes63.Data;
using ApiJokes63.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace ApiJokes63.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JokesController : ControllerBase
    {
        private readonly string _connectionString;
        public JokesController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConStr");
        }
        [HttpGet]
        [Route("getjoke")]
        public Joke GetJoke()
        {
            var client = new HttpClient();
            var json = client.GetStringAsync("https://jokesapi.lit-projects.com/jokes/programming/random").Result;
            var joke = JsonSerializer.Deserialize<List<Joke>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }).First();
            JokesRepository repo = new(_connectionString);
            var j = repo.AddJoke(joke);
            return j;
        }
        [HttpGet]
        [Route("getinteractionstatus/{jokeId}")]
        public object GetInteractionStatus(int jokeId)
        {
            UserJokeInteractionStatus status = GetStatus(jokeId);
            return new { status };
        }

        [HttpGet]
        [Route("viewall")]
        public List<Joke> ViewAll()
        {
            JokesRepository repo = new(_connectionString);
            return repo.ViewJokes();
        }
        [HttpGet]
        [Route("getlikescount/{jokeid}")]
        public object GetLikesCount(int jokeId)
        {
            var repo = new JokesRepository(_connectionString);
            var joke = repo.GetWithLike(jokeId);
            if(joke == null)
            {
                return null;
            }
            return new
            {
                likes = joke.UsersJokes.Count(u => u.Liked),
                dislikes = joke.UsersJokes.Count(u => !u.Liked)
            };
        }
        [HttpPost]
        [Authorize]
        [Route("interactwithjoke")]
        public void interactwithjoke(UsersJokes usersJokes)
        {
            JokesRepository repo = new(_connectionString);
            Repository repository = new(_connectionString);
            User user = repository.GetByEmail(User.Identity.Name);
            usersJokes.UserId = user.Id;
            repo.AddLike(usersJokes);          
        }
        private UserJokeInteractionStatus GetStatus(int jokeId)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return UserJokeInteractionStatus.Unauthenticated;
            }
            Repository repo = new(_connectionString);
            User user = repo.GetByEmail(User.Identity.Name);
            JokesRepository jokeRepo = new(_connectionString);
            UsersJokes likestatus = jokeRepo.GetLike(user.Id, jokeId);
            if(likestatus == null)
            {
                return UserJokeInteractionStatus.NeverInteracted;
            }
            return likestatus.Liked ? UserJokeInteractionStatus.Liked :
                UserJokeInteractionStatus.Disliked;

        }
    }
}

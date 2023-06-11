using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiJokes63.Data
{
    public class Joke
    {

        public int Id { get; set; }
        public int JokeId { get; set; }
        public List<UsersJokes> UsersJokes { get; set; }
        public string setUp { get; set; }
        public string punchLine { get; set; }
    }
}

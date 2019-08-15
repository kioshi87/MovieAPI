using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieAPI.MovieDbApiClient
{
    public class MovieDbApiMovie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Release_Date { get; set; }
        public string Overview { get; set; }
        public string Imdb_Id { get; set; }
        public string Poster_Path { get; set; }

    }
}

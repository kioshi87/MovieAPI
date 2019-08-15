using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieAPI.OmdbApi
{
    public class OmdbMovie
    {
        public string Title { get; set; }
        public string Year { get; set; }
        public string ImdbId { get; set; }
        public string Type { get; set; }
        public string Poster { get; set; }
        public string Released { get; set; }
        public string Genre { get; set; }
        public string Plot { get; set; }
        public string Director { get; set; }
        public string Metascore { get; set; }
        public string ImdbRating { get; set; } 


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieAPI.OmdbApi
{
    public class OmdbSearch
    {
        public List<OmdbMovie> Search { get; set; }
        public string TotalResults { get; set; }
        public string Response { get; set; }

    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MovieAPI.OmdbApi
{
    public class OmdbApi
    {
        //private readonly IHttpClientFactory _httpClientFactory;
        //private readonly HttpClient _client;

        private readonly string _apiKey = "1199e181";

        //public OmdbApi(IHttpClientFactory httpClientFactory)
        //{
        //    _httpClientFactory = httpClientFactory;
        //    _client = _httpClientFactory.CreateClient();

        //    refactor api key instead of hard coding
        //    _client.BaseAddress = new Uri("http://www.omdbapi.com/?apikey=1199e181");
        //}

        public async Task<List<OmdbMovie>> SearchMovie(string title, string year) 
        {            
            string uriString;

            if (year != null)
            {
                uriString = $"?apikey={_apiKey}&s={title}&y={year}";
            }
            else
            {
                uriString = $"?apikey={_apiKey}&s={title}";
            }

            var client = new HttpClient();
            client.BaseAddress = new Uri("http://www.omdbapi.com");
            var response = await client.GetAsync(uriString);
            var content = await response.Content.ReadAsStringAsync();
            var searchResults = JsonConvert.DeserializeObject<OmdbSearch>(content);
            var movieList = searchResults.Search.ToList();

            return movieList;

        }

        public async Task<OmdbMovie> GetMovieDetails(string imdbId)
        {
            //var omdbMovie = new OmdbMovie();
            var client = new HttpClient();
            client.BaseAddress = new Uri("http://www.omdbapi.com");
            var response = await client.GetAsync($"?apikey={_apiKey}&i={imdbId}");
            var content = await response.Content.ReadAsStringAsync();
            var omdbMovie = JsonConvert.DeserializeObject<OmdbMovie>(content);
            //var movieList = searchResults.Search.ToList();

            return omdbMovie;
        }


    }
}

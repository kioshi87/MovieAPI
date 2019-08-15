using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MovieAPI.MovieDbApiClient
{
    public class MovieDbApiClient
    {
        private readonly string _apiKey = "8dd6dc39ce15c95dadbc91a91895d078";

        public async Task<List<MovieDbApiMovie>> SearchMovieApi(string query)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://api.themoviedb.org");
            var response = await client.GetAsync($"3/search/movie?api_key={_apiKey}&language=en-US&query={query}&page=1&include_adult=false");
            var content = await response.Content.ReadAsStringAsync();
            var searchResults = JsonConvert.DeserializeObject<MovieDbApiSearch>(content);
            var movieList = searchResults.Results.ToList();

            return movieList;
        }

        public async Task<MovieDbApiMovie> GetMovieDetails(string id)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://api.themoviedb.org");
            var response = await client.GetAsync($"/3/movie/{id}?api_key={_apiKey}");
            var content = await response.Content.ReadAsStringAsync();
            var omdbMovie = JsonConvert.DeserializeObject<MovieDbApiMovie>(content);

            return omdbMovie;
        }

        public async Task<List<MovieDbApiMovie>> GetMovieRecommendations(string movieId)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://api.themoviedb.org");
            var response = await client.GetAsync($"3/movie/{movieId}/recommendations?api_key={_apiKey}&language=en-US&page=1&include_adult=false");
            var content = await response.Content.ReadAsStringAsync();
            var searchResults = JsonConvert.DeserializeObject<MovieDbApiSearch>(content);
            var movieList = searchResults.Results.ToList();

            return movieList;
        }

        public async Task<List<MovieDbApiMovie>> GetPopularMovies()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://api.themoviedb.org");
            var response = await client.GetAsync($"3/movie/popular?api_key={_apiKey}&language=en-US&page=1&include_adult=false");
            var content = await response.Content.ReadAsStringAsync();
            var searchResults = JsonConvert.DeserializeObject<MovieDbApiSearch>(content);
            var movieList = searchResults.Results.ToList();

            return movieList;
        }
    }
}

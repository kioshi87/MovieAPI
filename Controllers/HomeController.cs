using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MovieAPI.Models;
using MovieAPI.OmdbApi;

namespace MovieAPI.Controllers
{
    public class HomeController : Controller
    {
        public async Task<IActionResult> Index()
        {

            var moviedbapi = new MovieDbApiClient.MovieDbApiClient();
            var popularList = await moviedbapi.GetPopularMovies();
            ViewBag.PopularMovieList = popularList;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SearchMovieApi(string title, string year)
        {
            ViewBag.SearchString = title;
            var moviedbapi = new MovieDbApiClient.MovieDbApiClient();

            return View(await moviedbapi.SearchMovieApi(title));

        }

 
        public async Task<IActionResult> GetMovieDetails(string movieId)
        {
            var moviedbapi = new MovieDbApiClient.MovieDbApiClient();

            return View(await moviedbapi.GetMovieDetails(movieId));

        }

        public async Task<IActionResult> ToggleMovieFavorite(string movieId)
        {

            //call method to update database here

            return View("Index");
        }

        public async Task<IActionResult> GetMovieRecommendations(string movieId)
        {
            var moviedbapi = new MovieDbApiClient.MovieDbApiClient();
            var results = await moviedbapi.GetMovieRecommendations(movieId);

            return View("SearchMovieApi", results);
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

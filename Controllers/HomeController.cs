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
        public IActionResult Index()
        {


            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SearchMovieApi(string title, string year)
        {
            ViewBag.SearchString = title;
            var moviedbapi = new MovieDbApiClient.MovieDbApiClient();
            return View(await moviedbapi.SearchMovieApi(title));


            
            //if (!String.IsNullOrEmpty(title))
            //{
            //    var omdbApiClient = new OmdbApi.OmdbApiClient();
            //    return View(await omdbApiClient.SearchMovie(title, year));
            //}

            //return RedirectToAction("Index","Home");
            // add error handling in case no results

        }

 
        public async Task<IActionResult> GetMovieDetails(string id)
        {
            var moviedbapi = new MovieDbApiClient.MovieDbApiClient();
            return View(await moviedbapi.GetMovieDetails(id));

            //var omdbApiClient = new OmdbApi.OmdbApiClient();

            //return View(await omdbApiClient.GetMovieDetails(imdbId));

        }

        public async Task<IActionResult> ToggleMovieFavorite(string id)
        {

            //call method to update database here

            return View("Index");
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

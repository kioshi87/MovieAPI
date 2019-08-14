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
            
            if (!String.IsNullOrEmpty(title))
            {
                var omdbApi = new OmdbApi.OmdbApi();
                return View(await omdbApi.SearchMovie(title, year));
            }

            return RedirectToAction("Index","Home");
            // add error handling in case no results

        }

        public async Task<IActionResult> GetMovieDetails(string imdbId)
        {
            var omdbApi = new OmdbApi.OmdbApi();

            return View(await omdbApi.GetMovieDetails(imdbId));

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

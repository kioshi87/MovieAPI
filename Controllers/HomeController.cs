using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieAPI.Data;
using MovieAPI.Models;
using MovieAPI.OmdbApi;

namespace MovieAPI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISession _session;
        private readonly UserMovieDbContext _context;

        public HomeController(UserMovieDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _session = httpContextAccessor.HttpContext.Session;
            _context = context;
        }


        public async Task<IActionResult> Index()
        {

            var moviedbapi = new MovieDbApiClient.MovieDbApiClient();
            var popularList = await moviedbapi.GetPopularMovies();
            ViewBag.PopularMovieList = popularList;

            if (_session.GetInt32("currentUserId") != null)
            {
                var userId = (int)_session.GetInt32("currentUserId");
                var movieIdList = _context.UserMovie.Where(_ => _.UserId == userId)
                    .Select(m => m.MovieDbApiId).ToList();

                ViewBag.UsersFavorites = await moviedbapi.GetUsersMovies(movieIdList);

            }


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

        public async Task<IActionResult> ToggleMovieFavorite([Bind("Id,UserId,MovieDbApiId")] UserMovie userMovie)
        {          

            if (_session.GetInt32("currentUserId") != null)
            {
                userMovie.UserId = (int)_session.GetInt32("currentUserId");
                _context.Add(userMovie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction("Index");
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
        public async Task<IActionResult> SelectUser()
        {

            return View(await _context.User.ToListAsync());
        }

        public async Task<IActionResult> SetCurrentUser(int id)
        {
            var user = await _context.User
                .FirstOrDefaultAsync(m => m.Id == id);

            _session.SetInt32("currentUserId", user.Id);
            _session.SetString("currentUserFirstName", user.FirstName); 

            return RedirectToAction("Index");
        }
    }
}

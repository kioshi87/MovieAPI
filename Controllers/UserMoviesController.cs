using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MovieAPI.Data;
using MovieAPI.Models;

namespace MovieAPI.Controllers
{
    public class UserMoviesController : Controller
    {
        private readonly UserMovieDbContext _context;

        public UserMoviesController(UserMovieDbContext context)
        {
            _context = context;
        }

        // GET: UserMovies
        public async Task<IActionResult> Index()
        {
            var userMovieDbContext = _context.UserMovie.Include(u => u.User);
            return View(await userMovieDbContext.ToListAsync());
        }

        // GET: UserMovies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userMovie = await _context.UserMovie
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userMovie == null)
            {
                return NotFound();
            }

            return View(userMovie);
        }

        public IActionResult UpdateUserMovies(int? userId)
        {
            if (userId == null)
            {
                return NotFound();
            }

            var userMovie =  _context.UserMovie.Where(m => m.UserId == userId).ToList();

            if (userMovie == null)
            {
                return NotFound();
            }

            return View(userMovie);

        }

        // GET: UserMovies/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Id");
            return View();
        }

        public SelectList GetUserList()
        {
            //var userList = new SelectList(_context.User, "Id", "Id");
            return new SelectList(_context.User, "Id", "Id");
        }

        // POST: UserMovies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,MovieDbApiId")] UserMovie userMovie)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userMovie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Id", userMovie.UserId);
            return View(userMovie);
        }

        // GET: UserMovies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userMovie = await _context.UserMovie.FindAsync(id);
            if (userMovie == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Id", userMovie.UserId);
            return View(userMovie);
        }

        // POST: UserMovies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,MovieDbApiId")] UserMovie userMovie)
        {
            if (id != userMovie.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userMovie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserMovieExists(userMovie.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Id", userMovie.UserId);
            return View(userMovie);
        }

        // GET: UserMovies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userMovie = await _context.UserMovie
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userMovie == null)
            {
                return NotFound();
            }

            return View(userMovie);
        }

        // POST: UserMovies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userMovie = await _context.UserMovie.FindAsync(id);
            _context.UserMovie.Remove(userMovie);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserMovieExists(int id)
        {
            return _context.UserMovie.Any(e => e.Id == id);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using MovieAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieAPI.Data
{
    public class UserMovieDbContext : DbContext
    {
        public UserMovieDbContext(DbContextOptions<UserMovieDbContext> options)
            : base(options)
        {

        }

        public DbSet<User> User { get; set; }

        public DbSet<UserMovie> UserMovie { get; set; }

    }
}

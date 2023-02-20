using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace AspNet.Models
{
    public class MyMovieContext : DbContext
    {
        public DbSet<MyMovie> MyMovies { get; set; }
    }
}
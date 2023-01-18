using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace AspNet.Models
{
    public class MoviesDbInitializer : DropCreateDatabaseAlways<MovieContext>
    {

        protected override void Seed(MovieContext db)
        {
            db.Movies.Add(new Movie { Name = "Top Gun: Moverick", Author = "Joseph Kosinski", CreatedBy = "Universal Pictures", Date = DateTime.ParseExact("2022-12-10", "yyyy-mm-dd", null) });
            db.Movies.Add(new Movie { Name = "Turning Red", Author = "Domee Shi", CreatedBy = "Disney", Date = DateTime.ParseExact("2022-11-10", "yyyy-mm-dd", null) });
            db.Movies.Add(new Movie { Name = "The Batman", Author = "Matt Reeves", CreatedBy = "DC", Date = DateTime.ParseExact("2022-02-10", "yyyy-mm-dd", null) });
            db.Movies.Add(new Movie { Name = "The Woman King", Author = "Gina Prince", CreatedBy = "Universal Pictures", Date = DateTime.ParseExact("2022-01-10", "yyyy-mm-dd", null) });
            db.Movies.Add(new Movie { Name = "Prey", Author = "Dan Trachtenberg", CreatedBy = "Hulu", Date = DateTime.ParseExact("2022-09-10", "yyyy-mm-dd", null) });

            base.Seed(db);
        }
    }
}
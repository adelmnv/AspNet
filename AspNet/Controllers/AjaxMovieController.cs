using AspNet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AspNet.Controllers
{
    public class AjaxMovieController : Controller
    {
        private MyMovieContext db = new MyMovieContext();
        // GET: AjaxMovie
        public ActionResult Index()
        {
            ViewBag.List = db.MyMovies.Select(x=> x.Name).ToList();
            return View();
        }

        [HttpGet]
        public ActionResult MovieSearch()
        {
            return PartialView();
        }

        [HttpPost]
        public  ActionResult MovieSearch(string name)
        {
            var allMovies =  db.MyMovies.Where(x => x.Name.Contains(name)).ToList();
            if (allMovies.Count < 0)
            {
                return HttpNotFound();
            }
            return PartialView(allMovies);
        }

        public ActionResult FavoriteMovie()
        {
            return PartialView(db.MyMovies.First());
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
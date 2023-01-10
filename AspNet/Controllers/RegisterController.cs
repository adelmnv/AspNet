using AspNet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AspNet.Controllers
{
    public class RegisterController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.Date = DateTime.Now;
            return View();
        }

        [HttpPost]
        public string Index(Account account)
        {
            return "Thank you for registration " + account.FirstName + " "+ account.LastName + " !";
        }

       
    }
}
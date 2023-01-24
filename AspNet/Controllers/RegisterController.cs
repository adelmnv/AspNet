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
        private AccountContext db = new AccountContext();

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult SignUp()
        {
            ViewBag.Date = DateTime.Now;
            //ViewBag.Id = db.Accounts.Count() == 0 ? 1 : db.Accounts.Count()+1;
            return View();
        }

        [HttpPost]
        public string SignUp(Account account)
        {
            db.Accounts.Add(account);
            db.SaveChanges();
            return "Thank you for registration " + account.FirstName + " " + account.LastName + " !";
        }

        public ActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public string LogIn(string Email, string Password)
        {
            
            Account account = db.Accounts.Where(x=> x.Email == Email).FirstOrDefault();
            if(account == null) { return "Account is not found"; }
            return $"Hello {account.LastName} {account.FirstName}!";
        }

    }
}
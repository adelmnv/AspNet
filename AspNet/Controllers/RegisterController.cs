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
            return View();
        }

        [HttpPost]
        public ActionResult SignUp(Account account)
        {
            db.Accounts.Add(account);
            db.SaveChanges();
            return RedirectToAction("Index"); 
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
            return $"<p>Hello {account.LastName} {account.FirstName}!<div><a href=\"/Register/ViewAll\">View All</a> if you want to see all accounts</div>";
        }

        public ActionResult ViewAll()
        {
            if (db.Accounts.Count() == 0)
                return RedirectToAction("Index");
            return View(db.Accounts.ToList());
            
        }

        public ActionResult Details(int? id)
        {
            Account account = db.Accounts.Find(id);
            if (account == null)
                return HttpNotFound();
            return View(account);
        }
        
        public ActionResult Edit(int? id)
        {
            Account account = db.Accounts.Find(id);
            if (account == null)
                return HttpNotFound();
            return View(account);
        }

        [HttpPost]
        public ActionResult Edit([Bind(Include = "ID,FirstName,LastName,Password,Email,Gender,CreatedDate")] Account account)
        {
            if (ModelState.IsValid)
            {
                db.Entry(account).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ViewAll");
            }
            return View(account);
        }

        public ActionResult Delete(int? id)
        {
            Account account = db.Accounts.Find(id);
            if (account == null)
                return HttpNotFound();
            return View(account);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult ConfirmedDelete(int? id)
        {
            Account account = db.Accounts.Find(id);
            db.Accounts.Remove(account);
            db.SaveChanges();
            return RedirectToAction("ViewAll");
        }


    }
}
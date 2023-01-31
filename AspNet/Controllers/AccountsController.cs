using AspNet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AspNet.Controllers
{
    public class AccountsController : Controller
    {
        private ADPContext db = new ADPContext();
        public async Task<ActionResult> Index()
        {
            return View();
        }

        public async Task<ActionResult> LogIn()
        {
            return View();
        }

        public async Task<ActionResult> SignUp()
        {
            ViewBag.Date = DateTime.Now;
            return View();
        }

        [HttpPost]
        public async Task<ActionResult>SignUp([Bind(Include = "ID,FirstName,LastName,Password,Email,Gender,CreatedDate")] Account account)
        {
            if (ModelState.IsValid)
            {
                db.Accounts.Add(account);
                await db.SaveChangesAsync();

                Patient patient = new Patient();
                patient.Account= account;
                db.Patients.Add(patient);
                await db.SaveChangesAsync();

                return RedirectToAction("LogIn");
            }
            return View(account);

        }

        [HttpPost]
        public async Task<ActionResult> LogIn(string Email, string Password)
        {
            Account account = db.Accounts.Where(x => x.Email == Email && x.Password == Password).FirstOrDefault();
            if (account == null) { return HttpNotFound(); }
            if(db.Patients.Where(x=> x.AccountId == account.ID).Any())
            {
                int id = db.Patients.Where(x => x.AccountId == account.ID).First().Id;
                return RedirectToRoute(new { controller = "Patients", action = $"Details/{id}" });
            }
            else if(db.Doctors.Where(x => x.AccountId == account.ID).Any())
            {
                int id = db.Doctors.Where(x => x.AccountId == account.ID).First().Id;
                return RedirectToRoute(new { controller = "Doctors", action = $"Details/{id}", });
            }
            else if(account.ID== 1 && account.Password == "admin1") 
            {
                return RedirectToRoute(new { controller = "Admin", action = $"Index", });
            }
            return RedirectToAction("Index");
        }
    }
}
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AspNet.Models;

namespace AspNet.Controllers
{
    public class AdminController : Controller
    {
        private ADPContext db = new ADPContext();

        // GET: Admin
        public async Task<ActionResult> Index()
        {
            var doctors = db.Doctors.Include(d => d.Account);
            return View(await doctors.ToListAsync());
        }

        public async Task<ActionResult> PatientList()
        {
            var patients = db.Patients.Include(d => d.Account);
            return View(await patients.ToListAsync());
        }

        // GET: Admin/Create
        public ActionResult Create()
        {
            ViewBag.Date = DateTime.Now;
            return View();
        }

        // POST: Admin/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,FirstName,LastName,Password,Email,Gender,CreatedDate")] Account account, string Specialization)
        {
            if (ModelState.IsValid)
            {
                db.Accounts.Add(account);
                await db.SaveChangesAsync();
                Doctor doctor = new Doctor();
                doctor.Account = account;
                doctor.Specialization = Specialization;
                db.Doctors.Add(doctor);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(account);
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

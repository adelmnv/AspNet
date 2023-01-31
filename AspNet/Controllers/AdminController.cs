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
using System.Web.UI;
using System.Drawing.Printing;

namespace AspNet.Controllers
{
    public class AdminController : Controller
    {
        private ADPContext db = new ADPContext();

        // GET: Admin
        public async Task<ActionResult> Index(int page = 1)
        {
            //var doctors = db.Doctors.Include(d => d.Account);
            //return View(await doctors.ToListAsync());
            int pageSize = 5;
            IEnumerable<Doctor> doctors = db.Doctors.OrderBy(p => p.Id).Skip((page - 1) * pageSize).Take(pageSize).Include(d=> d.Account);
            PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = db.Doctors.Count() };
            IndexViewModel indexView = new IndexViewModel { PageInfo = pageInfo, Doctors = doctors };
            return View(indexView);
        }

        public async Task<ActionResult> PatientList(int page = 1)
        {
            //var patients = db.Patients.Include(d => d.Account);
            //return View(await patients.ToListAsync());
            int pageSize = 5;
            IEnumerable<Patient> patients = db.Patients.OrderBy(p => p.Id).Skip((page - 1) * pageSize).Take(pageSize).Include(d => d.Account);
            PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = db.Patients.Count() };
            IndexViewModel indexView = new IndexViewModel { PageInfo = pageInfo, Patients = patients };
            return View(indexView);
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

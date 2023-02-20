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
using Microsoft.Ajax.Utilities;
using AspNet.Filters;

namespace AspNet.Controllers
{
    public class PatientsController : Controller
    {
        private ADPContext db = new ADPContext();

        public async Task<ActionResult> Index()
        {
            return View();
        }
        // GET: Patients/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient patient = await db.Patients.FindAsync(id);
            if (patient == null)
            {
                return HttpNotFound();
            }
            patient.Account = await db.Accounts.FindAsync(patient.AccountId);
            patient.Doctors.ForEach(x=> x.Account = db.Accounts.Find(x.AccountId));
            ViewBag.Doctors = patient.Doctors;
            return View(patient);
        }

        // GET: Patients/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient patient = await db.Patients.FindAsync(id);
            if (patient == null)
            {
                return HttpNotFound();
            }
            patient.Account = await db.Accounts.FindAsync(patient.AccountId);
            ViewBag.AccountId = new SelectList(db.Accounts, "LastName", "FirstName", patient.AccountId);
            ViewBag.Doctors = db.Doctors.Include(a=> a.Account).ToList();

            return View(patient);
        }

        // POST: Patients/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Patient patient, int[] selectedDoctors)
        {
            Patient newpatient = db.Patients.Find(patient.Id);
            newpatient.Doctors.Clear();

            if (selectedDoctors != null)
            {
                foreach (var i in db.Doctors.Where(co => selectedDoctors.Contains(co.Id)))
                {
                    newpatient.Doctors.Add(i);
                }
            }

            db.Entry(newpatient).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction($"Details/{patient.Id}");
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

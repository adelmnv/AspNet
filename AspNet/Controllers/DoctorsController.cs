﻿using System;
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
    public class DoctorsController : Controller
    {
        private ADPContext db = new ADPContext();
        public async Task<ActionResult> Index(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Doctor doctor = await db.Doctors.FindAsync(id);
            if (doctor == null)
            {
                return HttpNotFound();
            }
            doctor.Account = await db.Accounts.FindAsync(doctor.AccountId);
            return View(doctor);
        }
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Doctor doctor = await db.Doctors.FindAsync(id);
            if (doctor == null)
            {
                return HttpNotFound();
            }
            doctor.Account = await db.Accounts.FindAsync(doctor.AccountId);
            doctor.Patients.ForEach(x => x.Account = db.Accounts.Find(x.AccountId));
            ViewBag.Patients = doctor.Patients;
            return PartialView(doctor);
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

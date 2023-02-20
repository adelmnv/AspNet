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
using AspNet.Filters;

namespace AspNet.Controllers
{
    public class AdminController : Controller
    {
        private ADPContext db = new ADPContext();

        // GET: Admin

        public string Index()
        {
            return "How you managed to reach this URL?";
        }

        [ActionName("EtoNeVhodDlyaAdmina")]
        public async Task<ActionResult> Index1(string gender, string state, int page = 1)
        {
            //var doctors = db.Doctors.Include(d => d.Account);
            //return View(await doctors.ToListAsync());

            int pageSize = 10;
            ViewBag.Gender = gender;
            ViewBag.State = state;
            IQueryable<Patient> patients = db.Patients.Include(x => x.Account);
            IQueryable<Doctor> doctors = db.Doctors.Include(x => x.Account);

            if(!String.IsNullOrEmpty(state) && !state.Equals("All"))
            {
                if(state == "Doctor")
                    patients = null;
                else if(state == "Patient")
                    doctors = null;
            }
            if(!String.IsNullOrEmpty(gender) && !gender.Equals("All"))
            {
                if(patients != null)
                    patients = patients.Where(x => x.Account.Gender == gender);
                if(doctors != null)
                    doctors = doctors.Where(x => x.Account.Gender == gender);
            }
            int size = 0;
            if (patients != null)
                size += patients.Count();
            if(doctors != null)
                size += doctors.Count();
          
            if(patients != null)
                patients = patients.OrderBy(p => p.Id).Skip((page - 1) * pageSize/2).Take(pageSize/2).Include(d => d.Account);
            if(doctors != null)
                doctors = doctors.OrderBy(p => p.Id).Skip((page - 1) * pageSize/2).Take(pageSize/2).Include(d => d.Account);
            PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = size };

            if(doctors != null && patients != null)
            {
                PatientDoctorListViewModel patientdoctorListViewModel = new PatientDoctorListViewModel
                {
                    Patients = patients.ToList(),
                    Doctors = doctors.ToList(),
                    Gender = new SelectList(new List<string>() { "All", "Woman", "Man" }),
                    State = new SelectList(new List<string>() { "All", "Doctor", "Patient" }),
                    PageInfo = pageInfo
                };
                return View(patientdoctorListViewModel);
            }
            else if (doctors == null)
            {
                PatientDoctorListViewModel patientdoctorListViewModel = new PatientDoctorListViewModel
                {
                    Patients = patients.ToList(),
                    Gender = new SelectList(new List<string>() { "All", "Woman", "Man" }),
                    State = new SelectList(new List<string>() { "All", "Doctor", "Patient" }),
                    PageInfo = pageInfo
                };
                return View(patientdoctorListViewModel);
            }
            else
            {
                PatientDoctorListViewModel patientdoctorListViewModel = new PatientDoctorListViewModel
                {
                    Doctors = doctors.ToList(),
                    Gender = new SelectList(new List<string>() { "All", "Woman", "Man" }),
                    State = new SelectList(new List<string>() { "All", "Doctor", "Patient" }),
                    PageInfo = pageInfo
                };
                return View(patientdoctorListViewModel);
            }
           
        }

        public async Task<ActionResult> DoctorList(int page = 1)
        {
            //var doctors = db.Doctors.Include(d => d.Account);
            //return View(await doctors.ToListAsync());
            int pageSize = 5;
            IEnumerable<Doctor> doctors = db.Doctors.OrderBy(p => p.Id).Skip((page - 1) * pageSize).Take(pageSize).Include(d => d.Account);
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

        public async Task<ActionResult> PatientFilter(string gender, int page = 1)
        {
            int pageSize = 5;
            ViewBag.Gender = gender;
            IQueryable<Patient> patients = db.Patients.Include(x => x.Account);

            if(gender != null)
            {
                if(gender != "All")
                    patients = patients.Where(x => x.Account.Gender == gender);
            }

            int size = patients.Count();
            patients = patients.OrderBy(p => p.Id).Skip((page - 1) * pageSize).Take(pageSize).Include(d => d.Account);
            
            PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = size };
            
            PatientListViewModel patientListViewModel = new PatientListViewModel
            {
                Patients = patients.ToList(),
                Gender = new SelectList(new List<string>() {"All", "Woman", "Man" }),
                PageInfo= pageInfo
            };

            return View(patientListViewModel);
        }

        public async Task<ActionResult> DoctorFilter(string gender, string specialization, int page = 1)
        {
            int pageSize = 5;
            ViewBag.Gender = gender;
            ViewBag.Specialization = specialization;
            List<string> specializations = db.Doctors.Select(x => x.Specialization).OrderBy(x=> x).ToList();
            specializations.Insert(0, "All");

            IQueryable<Doctor> doctors = db.Doctors.Include(x => x.Account);

            if(gender != null)
            {
                if (gender != "All")
                    doctors = doctors.Where(x => x.Account.Gender == gender);
            }

            if(!String.IsNullOrEmpty(specialization) && !specialization.Equals("All"))
            {
                doctors = doctors.Where(x=> x.Specialization== specialization);
            }

            int size = doctors.Count();

            doctors = doctors.OrderBy(p => p.Id).Skip((page - 1) * pageSize).Take(pageSize).Include(d => d.Account);
            PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = size };
            DoctorListViewModel doctorListViewModel = new DoctorListViewModel
            {
                Doctors= doctors.ToList(),
                Gender = new SelectList(new List<string>() { "All", "woman", "man" }),
                Specialization = new SelectList(specializations),
                PageInfo= pageInfo
            };
            return View(doctorListViewModel);
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

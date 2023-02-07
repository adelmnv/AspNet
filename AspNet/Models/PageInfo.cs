using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace AspNet.Models
{
    public class PageInfo
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages
        {
            get { return (int)Math.Ceiling((decimal)TotalItems / PageSize); }
        }
    }

    //Постраничный вывод
    public class IndexViewModel
    {
        public PageInfo PageInfo { get; set; }
        public IEnumerable<Doctor> Doctors { get; set; }
        public IEnumerable<Patient> Patients { get; set; }
    }

    public class PatientListViewModel
    {
        public IEnumerable<Patient> Patients { get; set; }
        public SelectList Gender { get; set; }
        public PageInfo PageInfo { get; set; }
    }

    public class DoctorListViewModel
    {
        public IEnumerable<Doctor> Doctors { get; set; }
        public SelectList Gender { get; set; }
        public SelectList Specialization { get; set; }
        public PageInfo PageInfo { get; set; }
    }

    public class PatientDoctorListViewModel
    {
        public IEnumerable<Doctor> Doctors { get; set; }
        public IEnumerable<Patient> Patients { get; set; }
        public SelectList State { get; set; }
        public SelectList Gender { get; set; }
        public PageInfo PageInfo { get; set; }
    }
}
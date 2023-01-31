using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace AspNet.Models
{
    public class Doctor
    {
        public int Id { get; set; }
        public Account Account { get; set; }
        public int? AccountId { get; set; }
        public string Specialization { get; set; }
        public virtual ICollection<Patient> Patients { get; set; }
        public Doctor() { Patients = new List<Patient>(); }
        public override bool Equals(object obj)
        {
            if (obj is Doctor doctor) return Id == doctor.Id;
            return false;
        }
    }
}
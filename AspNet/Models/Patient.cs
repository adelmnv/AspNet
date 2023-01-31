using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AspNet.Models
{
    public class Patient
    {
        public int Id { get; set; }
        public Account Account { get; set; }
        public int? AccountId { get; set; }
        public virtual ICollection<Doctor> Doctors { get; set; }
        public Patient() { Doctors = new List<Doctor>(); }
    }
}
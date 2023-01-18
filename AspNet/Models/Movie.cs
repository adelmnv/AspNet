using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AspNet.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public DateTime Date { get; set; }
        public string CreatedBy { get; set; }
    }
}
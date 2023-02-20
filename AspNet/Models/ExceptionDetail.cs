using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace AspNet.Models
{

    public class ExceptionDetail
    {
        public int Id { get; set; }
        public string ExceptionMessage { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public string StackTrace { get; set; }
        public DateTime Date { get; set; }
    }

    public class LogContext : DbContext
    {
        public LogContext() : base("DefaultConnection")
        {
        }
        public DbSet<ExceptionDetail> ExceptionDetails { get; set; }
    }
}
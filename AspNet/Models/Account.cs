﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AspNet.Models
{
    public class Account
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
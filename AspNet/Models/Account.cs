using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AspNet.Models
{
    public class Account
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "This is required field")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Min = 2, Max=50 simbols for field")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "This is required field")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Min = 2, Max=20 simbols for field")]
        public string LastName { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "This is required field")]
        [StringLength(15, MinimumLength = 6, ErrorMessage = "Min = 6, Max=15 simbols for field")]
        public string Password { get; set; }

        [Required(ErrorMessage = "This is required field")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[a-z]{2,4}", ErrorMessage = "Incorrect email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "This is required field")]
        [RegularExpression(@"^man|woman",ErrorMessage = "You can write only man or woman")]
        public string Gender { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
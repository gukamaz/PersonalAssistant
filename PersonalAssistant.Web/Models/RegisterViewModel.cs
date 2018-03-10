using PersonalAssistant.DAL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PersonalAssistant.Web.Models
{
    public class RegisterViewModel
    {
        public string PersonalID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EngFirstName { get; set; }
        public string EngLastName { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
        public Genders Gender { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
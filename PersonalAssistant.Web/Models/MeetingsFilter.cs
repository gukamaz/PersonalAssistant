using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PersonalAssistant.Web.Models
{
    public class MeetingsFilter
    {
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
    }
}
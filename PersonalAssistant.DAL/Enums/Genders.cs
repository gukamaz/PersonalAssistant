using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalAssistant.DAL.Enums
{
    public enum Genders
    {
        [Display(Name = "კაცი")]
        MALE = 1,

        [Display(Name = "ქალი")]
        FEMALE = 2,
    }
}

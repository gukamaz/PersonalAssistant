using PersonalAssistant.DAL.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalAssistant.DAL.Entities
{
    public class Person : BaseEntity
    {
        [MaxLength(250)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "პირადი ნომრის შეყვანა სავალდებულოა")]
        [Display(Name ="პირადი ნომერი")]
        public string PersonalID { get; set; }

        [MaxLength(250)]
        [Display(Name = "სახელი")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "სახელის შეყვანა სავალდებულოა")]
        public string FirstName { get; set; }

        [MaxLength(250)]
        [Display(Name = "გვარი")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "გვარის შეყვანა სავალდებულოა")]
        public string LastName { get; set; }

        [MaxLength(250)]
        [Display(Name = "სახელი (ინგლისურად)")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "ინგისური სახელის შეყვანა სავალდებულოა")]
        public string EngFirstName { get; set; }

        [MaxLength(250)]
        [Display(Name = "გვარი (ინგლისურად)")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "ინგლისური გვარის შეყვანა სავალდებულოა")]
        public string EngLastName { get; set; }

        [Display(Name = "დაბადების თარიღი")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "დაბადების თარიღის შეყვანა სავალდებულოა")]
        public DateTime BirtDate { get; set; }

        [Display(Name = "სქესი")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "სქესის შეყვანა სავალდებულოა")]
        public Genders Gender { get; set; }

        public string ImageUrl { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "პაროლის შეყვანა სავალდებულოა")]
        public string PasswordHash { get; set; }

        #region collections
        public virtual ICollection<Meeting> Meetings { get; set; }
        public virtual ICollection<Budget> Budgets { get; set; }
        #endregion
    }
}

using PersonalAssistant.DAL.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalAssistant.DAL.Entities
{
    public class Meeting : BaseEntity
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "თარიღის შეყვანა სავალდებულოა")]
        public DateTime Date { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "სახელის შეყვანა სავალდებულოა")]
        [MaxLength(250)]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "სტატუსის შეყვანა სავალდებულოა")]
        public MeetingStatuses Status { get; set; }

        public virtual Person Person { get; set; }

        #region foreign keys
        public int PersonID { get; set; }
        #endregion
    }
}

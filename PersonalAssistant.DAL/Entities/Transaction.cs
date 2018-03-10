using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalAssistant.DAL.Entities
{
    public class Transaction : BaseEntity
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "თარიღის გვარის შეყვანა სავალდებულოა")]
        public DateTime Date { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "თანხის გვარის შეყვანა სავალდებულოა")]
        public decimal Amount { get; set; }

        public virtual Budget Budget { get; set; }
        #region foreign keys
        public int BudgetID { get; set; }
        #endregion
    }
}

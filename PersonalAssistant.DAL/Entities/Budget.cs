using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalAssistant.DAL.Entities
{
    public class Budget : BaseEntity
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "თანხის შეყვანა სავალდებულოა")]
        public decimal Amount { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "თვის შეყვანა სავალდებულოა")]
        public int Month { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "წლის შეყვანა სავალდებულოა")]
        public int Year { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "სახელის შეყვანა სავალდებულოა")]
        public decimal SpentMoney { get; set; }

        public virtual Person Person { get; set; }

        #region foreign keys
        public int PersonID { get; set; }
        #endregion

        #region collections
        public virtual ICollection<Transaction> Transactions { get; set; }
        #endregion
    }
}

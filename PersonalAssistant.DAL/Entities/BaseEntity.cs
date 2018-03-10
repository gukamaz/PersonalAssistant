using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalAssistant.DAL.Entities
{
    public class BaseEntity
    {
        public BaseEntity()
        {
            CreateDate = DateTime.Now;
        }

        [Key]
        public int ID { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeleteDate { get; set; }
        public DateTime? LastUpdateDate { get; set; }
        public DateTime CreateDate { get; set; }
    }
}

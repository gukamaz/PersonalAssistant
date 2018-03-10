using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalAssistant.BLL.Models
{
    public class UserInfo
    {
        public int UserID;
        public DateTime LastActivity;
        public Guid Token;
    }
}

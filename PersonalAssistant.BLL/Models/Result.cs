using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalAssistant.BLL.Models
{
    public class Result
    {
        #region Constants
        public const string OK = "OK";
        public const string REMOVE_IS_FORBIDDEN = "წაშლა აღარ შეიძლება";
        public const string NO_AUTHORIZED = "ავტორიზაცია არ აქვს გავლილი";
        public const string UNKNOWN_ERROR = "მოხდა გაუთვალისწინებელი შეცდომა";
        #endregion

        public Result(bool isSuccess, string message, int code)
        {
            IsSuccess = isSuccess;
            Message = message;
            Code = code;
        }

        public Result()
        {

        }

        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public int Code { get; set; }
    }
}


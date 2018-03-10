using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalAssistant.BLL.Utils
{
    public interface IEncryptor
    {
        string HashPassword(string password);
        bool ValidatePassword(string password, string correctHash);
    }
}

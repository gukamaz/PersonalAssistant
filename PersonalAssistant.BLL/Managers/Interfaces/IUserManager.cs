using PersonalAssistant.BLL.Models;
using PersonalAssistant.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalAssistant.BLL.Managers
{
    public interface IUserManager : IDisposable
    {
        Result Register(Person person, string password, string confirmPassword);

        void Update(Person person);

        Result ChangePassword(int id, string oldPassword, string newPassword, string confirmPassword);

        Guid? Login(string username, string password);

        void Logout(Guid token);

        bool IsAuthorized(Guid token);

        UserInfo GetLogginedUser(Guid token);
    }
}

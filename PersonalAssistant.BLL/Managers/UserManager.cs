using PersonalAssistant.BLL.Models;
using PersonalAssistant.BLL.Utils;
using PersonalAssistant.DAL.Entities;
using PersonalAssistant.Repository;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalAssistant.BLL.Managers
{
    public class UserManager : IUserManager
    {
        #region Contsants
        const int MINUTES_AFTER_ACTIVITY = 20;

        const string RESULT_MESSAGE_INCORRECT_CONFIRMATION = "პაროლის დადასტურება არ ემთხვევა პაროლს.";
        const string RESULT_MESSAGE_CANT_FIND_USER = "ასეთი მომხმარებელი ვერ მოიძებნა";
        const string RESULT_MESSAGE_ALREADY_EXIST = "ასეთი მომხმარებელი უკვე არსებობს";
        const string RESULT_MESSAGE_CURRENT_PASSWORD_IS_INCORRECT = "მიმდინარე პაროლი არასწორია";
        const string RESULT_MESSAGE_PASSWORD_CHANGED = "პაროლი წარმატებით შეიცვალა";
        const string RESULT_MESSAGE_IS_NOT_ADULT = "რეგისტრაცია შესაძლებელია მხოლოდ სრულწლოვნებისთვის.";
        const string RESULT_MESSAGE_UNEXPECTED_ERROR = "დაფიქსირდა შეცდომა.";

        const int RESULT_CODE_PASSWORD_CHANGED = 0;
        const int RESULT_CODE_INCORRECT_CONFIRMATION = -1;
        const int RESULT_CODE_CANT_FIND_USER = -2;
        const int RESULT_CODE_CURRENT_PASSWORD_IS_INCORRECT = -3;
        const int RESULT_CODE_ALREADY_EXIST = -4;
        const int RESULT_CODE_IS_NOT_ADULT = -5;
        const int RESULT_CODE_UNEXPECTED_ERROR = -9;
        #endregion

        #region Fields
        private static ConcurrentDictionary<Guid, UserInfo> _users = new ConcurrentDictionary<Guid, UserInfo>();

        private IEncryptor _encryptor;
        private IUnitOfWork _uwork;
        #endregion

        #region Constructors
        public UserManager(IUnitOfWork uwork, IEncryptor encryptor)
        {
            _uwork = uwork;
            _encryptor = encryptor;
        }

        public UserManager(IEncryptor encryptor)
        {
            _encryptor = encryptor;
        }
        #endregion

        private bool IsAdult(DateTime birthdate)
        {
            var today = DateTime.Today;
            var age = today.Year - birthdate.Year;
            if (birthdate > today.AddYears(-age)) age--;
            return age >= 18;
        }

        private Result ValidatePersonForRegistration(Person person, string password, string confirmPassword)
        {
            if (password != confirmPassword)
                return new Result(false, RESULT_MESSAGE_INCORRECT_CONFIRMATION, RESULT_CODE_INCORRECT_CONFIRMATION);
            if (_uwork.Persons.Get(person.PersonalID) != null)
                return new Result(false, RESULT_MESSAGE_ALREADY_EXIST, RESULT_CODE_ALREADY_EXIST);
            if (!IsAdult(person.BirtDate))
                return new Result(false, RESULT_MESSAGE_IS_NOT_ADULT, RESULT_CODE_IS_NOT_ADULT);
            return new Result(true, Result.OK, 0);
        }

        public Result Register(Person person, string password, string confirmPassword)
        {
            var result = ValidatePersonForRegistration(person, password, confirmPassword);
            if (!result.IsSuccess)
                return result;
            try
            {
                person.PasswordHash = _encryptor.HashPassword(password);
                _uwork.Persons.Add(person);
                _uwork.Save();
                return new Result(true, Result.OK, 0);
            }
            catch
            {
                return new Result(true, RESULT_MESSAGE_UNEXPECTED_ERROR, RESULT_CODE_UNEXPECTED_ERROR);
            }
        }

        public void Update(Person person)
        {
            throw new NotImplementedException();
        }

        public Result ChangePassword(int id, string oldPassword, string newPassword, string confirmPassword)
        {
            if (newPassword != confirmPassword)
                return new Result(false, RESULT_MESSAGE_INCORRECT_CONFIRMATION, RESULT_CODE_INCORRECT_CONFIRMATION);

            var user = _uwork.Persons.Get(id);
            if (user == null)
                return new Result(false, RESULT_MESSAGE_CANT_FIND_USER, RESULT_CODE_CANT_FIND_USER);
            if (!_encryptor.ValidatePassword(oldPassword, user.PasswordHash))
                return new Result(false, RESULT_MESSAGE_CURRENT_PASSWORD_IS_INCORRECT, RESULT_CODE_CURRENT_PASSWORD_IS_INCORRECT);

            user.PasswordHash = _encryptor.HashPassword(newPassword);
            _uwork.Save();
            return new Result(true, RESULT_MESSAGE_PASSWORD_CHANGED, RESULT_CODE_PASSWORD_CHANGED);
        }

        public Guid? Login(string username, string password)
        {
            Guid? token = null;
            var user = _uwork.Persons.Get(u => u.PersonalID == username);
            if (user != null && _encryptor.ValidatePassword(password, user.PasswordHash))
            {
                token = Guid.NewGuid();
                _users.TryAdd(token.Value, new UserInfo()
                {
                    UserID = user.ID,
                    LastActivity = DateTime.Now,
                    Token = token.Value,
                });
            }
            return token;
        }

        public void Logout(Guid token)
        {
            UserInfo value;
            _users.TryRemove(token, out value);
        }

        public bool IsAuthorized(Guid token)
        {
            if (_users.ContainsKey(token) && _users[token].LastActivity > DateTime.Now.AddMinutes(-MINUTES_AFTER_ACTIVITY))
            {
                var userInfo = _users[token];
                userInfo.LastActivity = DateTime.Now;
                _users[token] = userInfo;
                return true;
            }

            return false;
        }

        public UserInfo GetLogginedUser(Guid token)
        {
            if (_users.ContainsKey(token) && _users[token].LastActivity > DateTime.Now.AddMinutes(-MINUTES_AFTER_ACTIVITY))
            {
                var userInfo = _users[token];
                userInfo.LastActivity = DateTime.Now;
                _users[token] = userInfo;
                return userInfo;
            }

            return null;
        }

        public void Dispose()
        {
            _uwork.Dispose();
        }
    }
}

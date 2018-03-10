using PersonalAssistant.BLL.Managers;
using PersonalAssistant.BLL.Utils;
using PersonalAssistant.DAL.Entities;
using PersonalAssistant.DAL.Enums;
using PersonalAssistant.Repository;
using PersonalAssistant.Web.Attributes;
using PersonalAssistant.Web.Extensions;
using PersonalAssistant.Web.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PersonalAssistant.Web.Controllers
{
    public class AccountController : Controller
    {
        private IUserManager _userManager;
        private IUnitOfWork _uwork;
        private ILangugageConverter _converter;

        public AccountController(IUnitOfWork uwork, IEncryptor encrypter, ILangugageConverter converter)
        {
            _userManager = new UserManager(uwork, encrypter);
            _converter = converter;
            _uwork = uwork;
        }

        [HttpGet]
        public ActionResult SignUp()
        {
            FillCalendar();
            FillGender();
            return View(new RegisterViewModel());
        }

        [HttpPost]
        public ActionResult SignUp(RegisterViewModel registerModel)
        {
            try
            {
                var person = ExtractModel(registerModel);
                var result = _userManager.Register(person, registerModel.Password, registerModel.ConfirmPassword);
                if (result.IsSuccess)
                    return RedirectToAction("SignIn");
                ViewBag.Error = result.Message;
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }

            FillCalendar();
            FillGender();
            return View(registerModel);
        }

        private void FillGender()
        {
            ViewBag.Genders = new List<SelectListItem>
            {
                new SelectListItem{Text = "კაცი", Value = "1"},
                new SelectListItem{Text = "ქალი", Value = "0"},
            };
        }

        private void FillCalendar()
        {
            var years = new List<SelectListItem>();
            for (int i = DateTime.Now.Year; i > 1900; i--)
                years.Add(new SelectListItem { Text = i.ToString(), Value = i.ToString() });

            var days = new List<SelectListItem>();
            for (int i = 1; i <= 31; i++)
                days.Add(new SelectListItem { Text = i.ToString(), Value = i.ToString() });

            ViewBag.Years = years;
            ViewBag.Days = days;
            ViewBag.Months = new List<SelectListItem>
            {
                new SelectListItem{Text = "იანვარი", Value = "1"},
                new SelectListItem{Text = "თებერვალი", Value = "2"},
                new SelectListItem{Text = "მარტი", Value = "3"},
                new SelectListItem{Text = "აპრილი", Value = "4"},
                new SelectListItem{Text = "მაისი", Value = "5"},
                new SelectListItem{Text = "ივნისი", Value = "6"},
                new SelectListItem{Text = "ივლისი", Value = "7"},
                new SelectListItem{Text = "აგვისტო", Value = "8"},
                new SelectListItem{Text = "სექტემბერი", Value = "9"},
                new SelectListItem{Text = "ოქტომბერი", Value = "10"},
                new SelectListItem{Text = "ნოემბერი", Value = "11"},
                new SelectListItem{Text = "დეკემბერი", Value = "12"},
            };
        }

        private Person ExtractModel(RegisterViewModel model)
        {
            var isCorrectDate = DateTime.TryParse($"{model.Year}-{model.Month}-{model.Day}", out DateTime date);
            if (!isCorrectDate)
                throw new Exception("მიუთითეთ თქვენი დაბადების თარიღი სწორად.");
            return new Person
            {
                PersonalID = model.PersonalID,
                BirtDate = date,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Gender = model.Gender,
                EngFirstName = _converter.Convert(model.FirstName),
                EngLastName = _converter.Convert(model.LastName),
                ImageUrl = "/images/profiles/usr.png"
            };
        }

        [HttpGet]
        public ActionResult SignIn()
        {
            return View(new LoginViewModel());
        }

        [HttpPost]
        public ActionResult SignIn(LoginViewModel loginModel)
        {
            var token = _userManager.Login(loginModel.UserName, loginModel.Password);
            if (token != null)
            {
                Response.SetCookie(AuthorizationExtension.LOGINTOKEN, token.Value.ToString());
                return RedirectToAction("Index");
            }
            ViewBag.Error = "პირადი ნომერი ან პაროლი არასწორია";
            return View(new LoginViewModel());
        }

        [HttpGet]
        [PAAuthorize]
        public ActionResult SignOut()
        {
            var token = this.GetLogginedUser().Token;
            _userManager.Logout(token);
            return RedirectToAction("SIgnIn");
        }

        [HttpGet]
        [PAAuthorize]
        public ActionResult Index()
        {
            var userID = this.GetLogginedUser().UserID;
            var person = _uwork.Persons.Get(userID);
            return View(person);
        }

        [HttpPost]
        [PAAuthorize]
        public ActionResult Index(Person person)
        {
            var userID = this.GetLogginedUser().UserID;
            var currentPerson = _uwork.Persons.Get(userID);
            currentPerson.EngFirstName = person.EngFirstName;
            currentPerson.EngLastName = person.EngLastName;
            currentPerson.Gender = person.Gender;
            currentPerson.LastName = person.LastName;
            currentPerson.FirstName = person.FirstName;
            _uwork.Save();
            return View(person);
        }

        private string SaveImage(HttpPostedFileBase file)
        {
            if (file != null)
            {
                string name = Guid.NewGuid().ToString() + ".jpg";
                string path = System.IO.Path.Combine(
                                       Server.MapPath("/images/profiles"), name);
                file.SaveAs(path);

                using (MemoryStream ms = new MemoryStream())
                {
                    file.InputStream.CopyTo(ms);
                    byte[] array = ms.GetBuffer();
                }
                return "/images/profiles/" + name;
            }
            return null;
        }

        public ActionResult FileUpload(HttpPostedFileBase file)
        {
            var personID = this.GetLogginedUser().UserID;
            var person = _uwork.Persons.Get(personID);
            person.ImageUrl = SaveImage(file);
            _uwork.Save();
            return RedirectToAction("Index");
        }
    }
}
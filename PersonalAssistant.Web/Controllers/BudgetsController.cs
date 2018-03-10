using PersonalAssistant.BLL.Models;
using PersonalAssistant.DAL.Entities;
using PersonalAssistant.Repository;
using PersonalAssistant.Web.Attributes;
using PersonalAssistant.Web.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PersonalAssistant.Web.Controllers
{
    [PAAuthorize]
    public class BudgetsController : Controller
    {
        private IUnitOfWork _uwork;

        public BudgetsController(IUnitOfWork uwork)
        {
            _uwork = uwork;
        }

        // GET: Budgets
        public ActionResult Index()
        {
            var budget = _uwork.Budgets.GetCurrentByPerson(PersonID);

            if (budget == null)
            {
                budget = new Budget
                {
                    Month = DateTime.Today.Month,
                    Year = DateTime.Today.Year,
                    PersonID = PersonID,
                };
                _uwork.Budgets.Add(budget);
                _uwork.Save();
            }
            if (budget.Transactions == null)
                budget.Transactions = new List<Transaction>();
            return View(budget);
        }


        [HttpPost]
        public ActionResult SaveBudget(decimal amount)
        {
            Result validationResult = ValidateBudget(amount);
            var currentBudget = _uwork.Budgets.GetCurrentByPerson(PersonID);
            if (validationResult.IsSuccess)
            {
                currentBudget.Amount = amount;
                _uwork.Save();
                ViewBag.BudgetSuccessMessage = "თანხა შენახულია";
            }
            else
                ViewBag.BudgetErrorMessage = validationResult.Message;
            return View("Index", currentBudget);
        }

        [HttpPost]
        public ActionResult SaveTransaction(Transaction transaction)
        {
            Result validationResult = ValidateTransaction(transaction.Amount);
            var currentBudget = _uwork.Budgets.GetCurrentByPerson(PersonID);
            if (validationResult.IsSuccess)
            {
                currentBudget.Transactions.Add(transaction);
                _uwork.Save();
                ViewBag.TrasnactionSuccessMessage = "თანხა შენახულია";
            }
            else
                ViewBag.TransactionErrorMessage = validationResult.Message;
            return View("Index", currentBudget);
        }


        private int _personID;

        private int PersonID
        {
            get
            {
                if (_personID == 0)
                    _personID = this.GetLogginedUser().UserID;
                return _personID;
            }
        }

        private Result ValidateTransaction(decimal budget)
        {
            Result result = new Result { Code = 0, Message = "OK", IsSuccess = true };

            if (budget == 0)
                result = new Result { Code = -1, IsSuccess = false, Message = "თანხა უნდა იყოს 0." };

            return result;
        }

        private Result ValidateBudget(decimal budget)
        {
            Result result = new Result { Code = 0, Message = "OK", IsSuccess = true };

            if (budget <= 0)
                result = new Result { Code = -1, IsSuccess = false, Message = "თანხა უნდა იყოს 0-ზე მეტი." };

            return result;
        }
    }
}
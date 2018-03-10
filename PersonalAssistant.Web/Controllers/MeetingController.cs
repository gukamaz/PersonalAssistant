using OfficeOpenXml;
using PersonalAssistant.BLL.Models;
using PersonalAssistant.DAL.Entities;
using PersonalAssistant.DAL.Enums;
using PersonalAssistant.Repository;
using PersonalAssistant.Web.Attributes;
using PersonalAssistant.Web.Extensions;
using PersonalAssistant.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PersonalAssistant.Web.Controllers
{
    [PAAuthorize]
    public class MeetingController : Controller
    {
        private IUnitOfWork _uwork;

        public MeetingController(IUnitOfWork uwork)
        {
            _uwork = uwork;
        }

        // GET: Meeting
        public ActionResult Index()
        {
            return View(GetOrderedMeetings());
        }

        // GET: Meeting
        [HttpPost]
        public ActionResult Index(Meeting meeting)
        {
            var personID = this.GetLogginedUser().UserID;
            meeting.PersonID = personID;
            meeting.Status = MeetingStatuses.PENDING;
            Result result = ValidateMeeting(meeting);
            if (result.IsSuccess)
            {
                _uwork.Meetings.Add(meeting);
                _uwork.Save();
                ViewBag.Success = "შეხვედრა ჩანიშნულია.";
            }
            else
                ViewBag.Error = result.Message;

            return View(GetOrderedMeetings());
        }

        private IQueryable<Meeting> GetOrderedMeetings()
        {
            var personID = this.GetLogginedUser().UserID;
            return _uwork.Meetings.GetByPerson(personID)
                                  .OrderByDescending(m => m.Date);
        }

        private Result ValidateMeeting(Meeting meeting)
        {
            var result = new Result { Code = 0, Message = "OK", IsSuccess = true };

            if (meeting.Date == DateTime.MinValue)
                result = new Result { Code = -1, Message = "გთხოვთ, მიუთითოთ თარიღი.", IsSuccess = false };

            else if (string.IsNullOrEmpty(meeting.Name))
                result = new Result { Code = -2, Message = "გთხოვთ, მიუთითოთ სახელი", IsSuccess = false };

            return result;
        }

        public ActionResult Done(int id)
        {
            UpdateStatus(id, MeetingStatuses.DONE);
            return RedirectToAction("Index");
        }

        public ActionResult Canceled(int id)
        {
            UpdateStatus(id, MeetingStatuses.CANCELED);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public FileStreamResult Export(MeetingsFilter filter)
        {
            MemoryStream memStream;

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Report");

                var personID = this.GetLogginedUser().UserID;
                var meetings = _uwork.Meetings.GetRange(m => m.PersonID == personID
                                                          && (filter.From == null || DbFunctions.TruncateTime(m.Date) >= filter.From)
                                                          && (filter.To == null || DbFunctions.TruncateTime(m.Date) <= filter.To))
                                                          .OrderByDescending(m => m.Date);


                worksheet.Cells[1, 1].Value = "თარიღი";
                worksheet.Cells[1, 2].Value = "სახელი და გვარი";
                worksheet.Cells[1, 3].Value = "სტატუსი";

                int index = 1;
                foreach (var meeting in meetings)
                {
                    ++index;
                    worksheet.Cells[index, 1].Value = meeting.Date;
                    worksheet.Cells[index, 2].Value = meeting.Name;
                    worksheet.Cells[index, 3].Value = meeting.Status == MeetingStatuses.CANCELED ? "არ შევხვდეი"
                                                : meeting.Status == MeetingStatuses.DONE ? "შევხვდი" : "მიმდინარე";
                }

                memStream = new MemoryStream(package.GetAsByteArray());
                return File(memStream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
            }

        }

        private void UpdateStatus(int id, MeetingStatuses status)
        {
            var meeting = _uwork.Meetings.Get(id);
            meeting.Status = status;
            _uwork.Save();
        }
    }
}
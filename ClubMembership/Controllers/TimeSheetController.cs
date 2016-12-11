using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ClubMembership.DAL;
using ClubMembership.Models;
using PagedList;
using System.Data.Entity.Infrastructure;
using System.Runtime.CompilerServices;

namespace ClubMembership.Controllers
{
    public class TimeSheetController : Controller
    {
        private MembershipContext db = new MembershipContext();

        // GET: Member
        public ActionResult Index(string sortOrder, string searchString, string currentFilter, int? page, string type,
            bool topCondition = false)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.FullNameSortParm = String.IsNullOrEmpty(sortOrder) ? "fullname_desc" : "";
            ViewBag.DateSortParm = sortOrder == "recordstartdate" ? "recordstartdate_desc" : "recordstartdate";
            ViewBag.DateEndSortParm = sortOrder == "recordenddate" ? "recordenddate_desc" : "recordenddate";

            int pageSize = 10;
            int pageNumber = (page ?? 1);

            if (topCondition == true)
            {
                 
                if (type == "AllToday")
                {
                    var today = DateTime.Today;
                    var todaysTimeSheet = db.TimeSheets.Where(m => DbFunctions.TruncateTime(m.RecordStartDate) == today).OrderByDescending(m => m.RecordStartDate);
                    return View(todaysTimeSheet.ToPagedList(pageNumber, pageSize));
                }
                if (type == "ActiveEntry")
                {
                   var todaysTimeSheet = db.TimeSheets.Where(m => m.RecordEndDate == null).OrderByDescending(m => m.RecordStartDate);
                   return View(todaysTimeSheet.ToPagedList(pageNumber, pageSize));
                }
                else
                {
                    var timesheets = from m in db.TimeSheets select m;
                    return View(timesheets.ToPagedList(pageNumber, pageSize));
                }
                
            }
            else
            {
                if (searchString != null)
                {
                    page = 1;
                }
                else
                {
                    searchString = currentFilter;
                }

                ViewBag.CurrentFilter = searchString;

                var timesheets = from m in db.TimeSheets select m;

       

                if (!String.IsNullOrEmpty(searchString))
                {
           
                    timesheets = timesheets.Where(s => ((s.Member.FirstName)+  " " + (s.Member.LastName)).Contains(searchString));
                }
                switch (sortOrder)
                {
                    case "fullname_desc":
                        timesheets = timesheets.OrderByDescending(m => m.Member.FirstName);
                        break;
                    case "fullname":
                        timesheets = timesheets.OrderBy(m => m.Member.FirstName);
                        break;
                    case "recordenddate_desc":
                        timesheets = timesheets.OrderByDescending(m => m.RecordEndDate);
                        break;
                    case "recordenddate":
                        timesheets = timesheets.OrderBy(m => m.RecordEndDate);
                        break;
                    case "recordstartdate_desc":
                        timesheets = timesheets.OrderByDescending(m => m.RecordStartDate);
                        break;
                    default:
                        timesheets = timesheets.OrderBy(m => m.RecordStartDate);
                        break;
                }
                return View(timesheets.ToPagedList(pageNumber, pageSize));
            } 
        }



        // GET: Campaign/Create
        public ActionResult Create(int? id)
        {
            ViewBag.MemberId = new SelectList(db.Members
                        .Where(o => o.Deleted == false).ToList(), "Id", "Fullname", id);
            return View();
        }

        // POST: Timesheet/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TimesheetId,RecordStartDate,MemberId")] Timesheet timesheet)
        {

            timesheet.RecordStartDate = DateTime.UtcNow;
            if (ModelState.IsValid)
            {
                db.TimeSheets.Add(timesheet);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MemberId = new SelectList(db.Members
                        .Where(o => o.Deleted == false).ToList(), "Id", "Fullname", timesheet.Member);
            return View(timesheet);
        }

        // GET: Campaign/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Timesheet timesheet = db.TimeSheets.Find(id);
            if (timesheet == null)
            {
                return HttpNotFound();
            }
           // ViewBag.EditionId = new SelectList(db.Editions, "EditionId", "Title", timesheet.EditionId);
            return View(timesheet);
        }

        // POST: Campaign/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TimesheetId")] Timesheet timesheet)
        {

            if (timesheet.TimeSheetId == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            
            var timesheetEntryToUpdate = db.TimeSheets.Single(i => i.TimeSheetId == timesheet.TimeSheetId);
            if (TryUpdateModel(timesheetEntryToUpdate, "", new string[] { "RecordEndDate" }))
            {
                try
                {
                    timesheetEntryToUpdate.RecordEndDate = DateTime.UtcNow;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (RetryLimitExceededException)
                {
                    ModelState.AddModelError("", "Unable to save changes");
                }
            }
             
            return View(timesheet);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

     
     
    }
}

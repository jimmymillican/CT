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
    public class PaymentController : Controller
    {
        private MembershipContext db = new MembershipContext();

        // GET: Member
        public ActionResult Index(string sortOrder, string searchString, string currentFilter, int? page, string type, bool topCondition = false)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.FullNameSortParm = String.IsNullOrEmpty(sortOrder) ? "fullname_desc" : "";
            ViewBag.DateSortParm = sortOrder == "recordstartdate" ? "recordstartdate_desc" : "recordstartdate";
            ViewBag.DateEndSortParm = sortOrder == "recordenddate" ? "recordenddate_desc" : "recordenddate";

            int pageSize = 10;
            int pageNumber = (page ?? 1);

            if (topCondition == true)
            {

                //if (type == "AllToday")
                //{
                //    var today = DateTime.Today;
                //    var todaysTimeSheet = db.TimeSheets.Where(m => DbFunctions.TruncateTime(m.RecordStartDate) == today).OrderByDescending(m => m.RecordStartDate);
                //    return View(todaysTimeSheet.ToPagedList(pageNumber, pageSize));
                //}
                //if (type == "ActiveEntry")
                //{
                //   var todaysTimeSheet = db.TimeSheets.Where(m => m.RecordEndDate == null).OrderByDescending(m => m.RecordStartDate);
                //   return View(todaysTimeSheet.ToPagedList(pageNumber, pageSize));
                //}
                //else
                //{
                //    var timesheets = from m in db.TimeSheets select m;
                //    return View(timesheets.ToPagedList(pageNumber, pageSize));
                //}
                var memberAccountPayment = from m in db.MemberAccountPayment select m;
                return View(memberAccountPayment.ToPagedList(pageNumber, pageSize));
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

                var memberAccountPayment = from m in db.MemberAccountPayment select m;




                if (!String.IsNullOrEmpty(searchString))
                {

                   // from t1 in db.Table1
                    //join t2 in db.Table2 on t1.field equals t2.field
                    //select new { t1.field2, t2.field3 }
                     
                     memberAccountPayment = from a in db.MemberAccountPayment
                                            join acc in db.MemberAccount on a.MemberAccountId equals acc.MemberAccountId
                                            join mem in db.Members on acc.MemberId equals mem.Id
                                        where mem.FullName.Contains(searchString)
                                         select a;

                   // memberAccountPayment = memberAccountPayment.Where(s => ((s.memberAccount) + " " + (s.MemberId.LastName)).Contains(searchString));
                }

                memberAccountPayment = from a in db.MemberAccountPayment
                                       join acc in db.MemberAccount on a.MemberAccountId equals acc.MemberAccountId
                                       join mem in db.Members on acc.MemberId equals mem.Id
                                       select a;
                switch (sortOrder)
                {
                //    case "fullname_desc":
                //        memberAccountPayment = memberAccountPayment.OrderByDescending(m => m.MemberId.FirstName);
                //        break;
                //    case "fullname":
                //        memberAccountPayment = memberAccountPayment.OrderBy(m => m.MemberId.FirstName);
                //        break;
                   default:
                        memberAccountPayment = memberAccountPayment.OrderByDescending(m => m.PaymentDate);
                        break;
                }
                return View(memberAccountPayment.ToPagedList(pageNumber, pageSize));
            } 
        }
         

        // GET: Campaign/Create
        public ActionResult Create(int? id)
        {
           ViewBag.MemberAccountId = new SelectList(db.MemberAccount
                       .ToList(), "MemberAccountId", "MemberAccountId");
            ViewBag.PaymentMethodId = new SelectList(db.PaymentMethod, "PaymentMethodId", "Description");
            ViewBag.PaymentStatusId = new SelectList(db.PaymentStatus
                       .ToList(), "PaymentStatusId", "Description"); 
            return View();
        }

        // POST: Timesheet/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MemberAccountId,MemberId,PaymentMethodId,PaymentStatusId,PaymentDate,Amount,AdditionalDetails")] MemberAccountPayment memberAccountPayment)
        {
            memberAccountPayment.PaymentDate = DateTime.UtcNow;
            if (ModelState.IsValid)
            {
                db.MemberAccountPayment.Add(memberAccountPayment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AccountId = new SelectList(db.MemberAccount
                       .ToList(), "MemberAccountId", "MemberAccountId", memberAccountPayment.MemberAccountId);
            ViewBag.PaymentMethodId = new SelectList(db.PaymentMethod
                       .ToList(), "PaymentMethodId", "Description", memberAccountPayment.PaymentMethodId);
            ViewBag.PaymentStatusId = new SelectList(db.PaymentStatus
                       .ToList(), "PaymentStatusId", "Description", memberAccountPayment.PaymentStatusId);

            return View();
        }

        //// GET: Campaign/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Timesheet timesheet = db.TimeSheets.Find(id);
        //    if (timesheet == null)
        //    {
        //        return HttpNotFound();
        //    }
        //   // ViewBag.EditionId = new SelectList(db.Editions, "EditionId", "Title", timesheet.EditionId);
        //    return View(timesheet);
        //}

        //// POST: Campaign/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "TimesheetId")] Timesheet timesheet)
        //{

        //    if (timesheet.TimeSheetId == 0)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }


        //    var timesheetEntryToUpdate = db.TimeSheets.Single(i => i.TimeSheetId == timesheet.TimeSheetId);
        //    if (TryUpdateModel(timesheetEntryToUpdate, "", new string[] { "RecordEndDate" }))
        //    {
        //        try
        //        {
        //            timesheetEntryToUpdate.RecordEndDate = DateTime.UtcNow;
        //            db.SaveChanges();
        //            return RedirectToAction("Index");
        //        }
        //        catch (RetryLimitExceededException)
        //        {
        //            ModelState.AddModelError("", "Unable to save changes");
        //        }
        //    }

        //    return View(timesheet);
        //}

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

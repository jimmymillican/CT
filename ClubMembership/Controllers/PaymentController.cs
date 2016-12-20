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

            var memberAccountPayment = from m in db.MemberAccountPayment select m;

            memberAccountPayment = from a in db.MemberAccountPayment
                                   join acc in db.MemberAccount on a.MemberAccountId equals acc.MemberAccountId
                                   join mem in db.Members on acc.MemberId equals mem.Id
                                   select a;

            if (topCondition == true)
            {

                if (type == "Last30Days")
                {
                   var memberAccountPayment30 = (from a in db.MemberAccountPayment
                                           join acc in db.MemberAccount on a.MemberAccountId equals acc.MemberAccountId
                                           join mem in db.Members on acc.MemberId equals mem.Id
                                           where DbFunctions.DiffDays(a.PaymentDate, DateTime.Now) < 30
                                           select a).OrderByDescending(m => m.MemberAccountPaymentId);

                    return View(memberAccountPayment30.ToPagedList(pageNumber, pageSize));
                }
                if (type == "Last12Months")
                {
                    var memberAccountPayment12 = (from a in db.MemberAccountPayment
                                                 join acc in db.MemberAccount on a.MemberAccountId equals acc.MemberAccountId
                                                 join mem in db.Members on acc.MemberId equals mem.Id
                                                 where DbFunctions.DiffMonths(a.PaymentDate, DateTime.Now) < 12
                                                 select a).OrderByDescending(m => m.MemberAccountPaymentId);
                    return View(memberAccountPayment12.ToPagedList(pageNumber, pageSize));
                }
                else
                {
                    var memberAccountPaymentDefault = (from a in db.MemberAccountPayment
                                           join acc in db.MemberAccount on a.MemberAccountId equals acc.MemberAccountId
                                           join mem in db.Members on acc.MemberId equals mem.Id
                                           select a).OrderByDescending(m => m.MemberAccountPaymentId);
                    return View(memberAccountPaymentDefault.ToPagedList(pageNumber, pageSize));
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
                 

                if (!String.IsNullOrEmpty(searchString))
                {
                     
                     memberAccountPayment = from a in db.MemberAccountPayment
                                            join acc in db.MemberAccount on a.MemberAccountId equals acc.MemberAccountId
                                            join mem in db.Members on acc.MemberId equals mem.Id
                                        where (mem.FirstName + " " + mem.LastName).Contains(searchString)
                                         select a;

                    var memberAccountPaymentTotal = memberAccountPayment.Sum(d => d.Amount);
                    ViewBag.memberAccountPaymentTotal = memberAccountPaymentTotal;
                }

               
                switch (sortOrder)
                {
                    //case "fullname_desc":
                    //    memberAccountPayment = memberAccountPayment.OrderByDescending(m => m.MemberId.FirstName);
                    //    break;
                    //case "fullname":
                    //    memberAccountPayment = memberAccountPayment.OrderBy(m => m.MemberId.FirstName);
                    //    break;
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

            //ViewBag.AccountId = new SelectList(db.MemberAccount
            //           .ToList(), "MemberAccountId", "MemberAccountId", mem.AccountFullDescription);
            //ViewBag.PaymentMethodId = new SelectList(db.PaymentMethod
            //           .ToList(), "PaymentMethodId", "Description", memberAccountPayment.PaymentMethodId);
            //ViewBag.PaymentStatusId = new SelectList(db.PaymentStatus
            //           .ToList(), "PaymentStatusId", "Description", memberAccountPayment.PaymentStatusId);

            return View();
        }

        // GET: Payment/Display
        public ActionResult Display(int? id)
        {
            MemberAccountPayment payment = db.MemberAccountPayment.Find(id);
            return View(payment);
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

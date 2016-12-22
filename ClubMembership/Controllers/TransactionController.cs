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
    public class TransactionController : Controller
    {
        private MembershipContext db = new MembershipContext();


        //// GET: Member
        //public ActionResult Index(int memberid, int accountid, int? page)
        //{
        //    var memberAccountPayment = from a in db.MemberAccountTransactions
        //                           join acc in db.MemberAccount on a.MemberAccountId equals acc.MemberAccountId
        //                           join mem in db.Members on acc.MemberId equals mem.Id
        //                           where (a.TransactionTypeId == 2)
        //                           select a;
        //    int pageSize = 10;
        //    int pageNumber = (page ?? 1);
        //    return View(memberAccountPayment.ToPagedList(pageNumber, pageSize));
        //}
        // GET: Member
        public ActionResult Index(int? memberId, int? memberAccount ,string sortOrder, string searchString, string currentFilter, int? page, string type, bool topCondition = false)
        {
            ViewBag.MemberId = memberId;
            ViewBag.MemberAccount = memberAccount;
            ViewBag.CurrentSort = sortOrder;
            ViewBag.FullNameSortParm = String.IsNullOrEmpty(sortOrder) ? "fullname_desc" : "";
            ViewBag.DateSortParm = sortOrder == "recordstartdate" ? "recordstartdate_desc" : "recordstartdate";
            ViewBag.DateEndSortParm = sortOrder == "recordenddate" ? "recordenddate_desc" : "recordenddate";

            int pageSize = 10;
            int pageNumber = (page ?? 1);

            var memberAccountPayment = from m in db.MemberAccountTransactions select m;

            memberAccountPayment = from a in db.MemberAccountTransactions
                                   join acc in db.MemberAccount on a.MemberAccountId equals acc.MemberAccountId
                                   join mem in db.Members on acc.MemberId equals mem.Id
                                   where acc.MemberId == memberId && acc.MemberAccountId == memberAccount
                                   select a;

            if (memberAccountPayment.Any())
            {
                var accountBalance = memberAccountPayment.Sum(d => d.Amount);
                ViewBag.AccountBalance = accountBalance;
            }
            

            if (topCondition == true)
            {

                if (type == "Last30Days")
                {
                   var memberAccountPayment30 = (from a in db.MemberAccountTransactions
                                           join acc in db.MemberAccount on a.MemberAccountId equals acc.MemberAccountId
                                           join mem in db.Members on acc.MemberId equals mem.Id
                                           where  acc.MemberId == memberId && acc.MemberAccountId == memberAccount && DbFunctions.DiffDays(a.TransactionDate, DateTime.Now) < 30
                                           select a).OrderByDescending(m => m.MemberAccountTransactionId);

                    return View(memberAccountPayment30.ToPagedList(pageNumber, pageSize));
                }
                if (type == "Last12Months")
                {
                    var memberAccountPayment12 = (from a in db.MemberAccountTransactions
                                                 join acc in db.MemberAccount on a.MemberAccountId equals acc.MemberAccountId
                                                 join mem in db.Members on acc.MemberId equals mem.Id
                                                 where  acc.MemberId == memberId && acc.MemberAccountId == memberAccount && DbFunctions.DiffMonths(a.TransactionDate, DateTime.Now) < 12
                                                 select a).OrderByDescending(m => m.MemberAccountTransactionId);
                    return View(memberAccountPayment12.ToPagedList(pageNumber, pageSize));
                }
                else
                {
                    var memberAccountPaymentDefault = (from a in db.MemberAccountTransactions
                                                       join acc in db.MemberAccount on a.MemberAccountId equals acc.MemberAccountId
                                                       join mem in db.Members on acc.MemberId equals mem.Id
                                                       where  acc.MemberId == memberId && acc.MemberAccountId == memberAccount
                                                       select a).OrderByDescending(m => m.MemberAccountTransactionId);
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
                     
                     memberAccountPayment = from a in db.MemberAccountTransactions
                                            join acc in db.MemberAccount on a.MemberAccountId equals acc.MemberAccountId
                                            join mem in db.Members on acc.MemberId equals mem.Id
                                        where  (mem.FirstName + " " + mem.LastName).Contains(searchString)
                                         select a;

                    
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
                        memberAccountPayment = memberAccountPayment.OrderByDescending(m => m.TransactionDate);
                        break;
                }
                return View(memberAccountPayment.ToPagedList(pageNumber, pageSize));
            } 
        }
         

        // GET: Campaign/Create
        public ActionResult Create(int? id, int? memberId, int? accountId)
        {
            ViewBag.PaymentMethodId = new SelectList(db.PaymentMethod, "PaymentMethodId", "Description");
            ViewBag.PaymentStatusId = new SelectList(db.PaymentStatus
                       .ToList(), "PaymentStatusId", "Description");
            ViewBag.MemberId = memberId; 
            return View();
        }

        // POST: Timesheet/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MemberAccountId,TransactionTypeId,PaymentMethodId,PaymentStatusId,TransactionDate,Amount,AdditionalDetails")] MemberAccountTransaction memberAccountPayment)
        {
            memberAccountPayment.TransactionTypeId = 1;
            memberAccountPayment.TransactionDate = DateTime.UtcNow;
           
            if (ModelState.IsValid)
            {
                var memberId = Request["hidMemberId"];
                db.MemberAccountTransactions.Add(memberAccountPayment);
                db.SaveChanges();
                return RedirectToAction("Index", new { memberId });
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
            MemberAccountTransaction payment = db.MemberAccountTransactions.Find(id);
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

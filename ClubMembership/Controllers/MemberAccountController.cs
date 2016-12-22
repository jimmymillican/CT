using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ClubMembership.DAL;
using ClubMembership.Models;
using PagedList;
using System.Data.Entity.Infrastructure;
using ClubMembership.Migrations;

namespace ClubMembership.Controllers
{
    public class MemberAccountController : Controller
    {

        private MembershipContext db = new MembershipContext();


        public ActionResult Index()
        {
            return View();
        }

        // GET: Campaign/Create
        public ActionResult Create(int? memberId)
        {


            var AccountCheck = (from a in db.MemberAccount
                                where a.MemberId == memberId
                                      && a.Suspended == false
                                      && (a.EndDate == null || a.EndDate <= DateTime.UtcNow)
                                select a);
            if (AccountCheck.Any())
            {
                ViewBag.WarningMessage = "*** An Active account already exists for this user ****";
            } 
            

            Member member = db.Members.Find(memberId);
            ViewBag.AccountTypeId_AccountTypeId = new SelectList(db.AccountType, "AccountTypeId", "Description");
            ViewBag.MemberId = memberId;
            ViewBag.MemberFullName = member.FullName;
            return View();
        }

        // POST: Timesheet/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AccountTypeId,MemberId,StartDate,Amount,AdditionalDetails")] MemberAccount memberAccount)
        {
        
            memberAccount.StartDate = DateTime.UtcNow;

            var memberId = Request["hidMemberId"];
            memberAccount.MemberId = Int32.Parse(memberId);


            if (ModelState.IsValid)
            {
                db.MemberAccount.Add(memberAccount);
                db.SaveChanges();
                return RedirectToAction("Details","Member", new { id = memberId });
            }
 

            return View();
        }

        // GET: Member
        public ActionResult Charge(string sortOrder, string searchString, string currentFilter, int? page, string type, bool topCondition = false)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.FullNameSortParm = String.IsNullOrEmpty(sortOrder) ? "fullname_desc" : "";
            ViewBag.DateSortParm = sortOrder == "recordstartdate" ? "recordstartdate_desc" : "recordstartdate";
            ViewBag.DateEndSortParm = sortOrder == "recordenddate" ? "recordenddate_desc" : "recordenddate";

            int pageSize = 10;
            int pageNumber = (page ?? 1);

            var memberAccountCharge = from m in db.MemberAccountCharge select m;

            memberAccountCharge = from a in db.MemberAccountCharge
                                   join acc in db.MemberAccount on a.MemberAccountId equals acc.MemberAccountId
                                   join mem in db.Members on acc.MemberId equals mem.Id
                                   select a;

            if (topCondition == true)
            {

                if (type == "Last30Days")
                {
                    var memberAccountCharge30 = (from a in db.MemberAccountCharge
                                                  join acc in db.MemberAccount on a.MemberAccountId equals acc.MemberAccountId
                                                  join mem in db.Members on acc.MemberId equals mem.Id
                                                  where DbFunctions.DiffDays(a.ChargeDate, DateTime.Now) < 30
                                                  select a).OrderByDescending(m => m.MemberAccountChargeId);

                    return View(memberAccountCharge30.ToPagedList(pageNumber, pageSize));
                }
                if (type == "Last12Months")
                {
                    var memberAccountCharge12 = (from a in db.MemberAccountCharge
                                                  join acc in db.MemberAccount on a.MemberAccountId equals acc.MemberAccountId
                                                  join mem in db.Members on acc.MemberId equals mem.Id
                                                  where DbFunctions.DiffMonths(a.ChargeDate, DateTime.Now) < 12
                                                  select a).OrderByDescending(m => m.MemberAccountChargeId);
                    return View(memberAccountCharge12.ToPagedList(pageNumber, pageSize));
                }
                else
                {
                    var memberAccountChargeDefault = (from a in db.MemberAccountCharge
                                                       join acc in db.MemberAccount on a.MemberAccountId equals acc.MemberAccountId
                                                       join mem in db.Members on acc.MemberId equals mem.Id
                                                       select a).OrderByDescending(m => m.MemberAccountChargeId);
                    return View(memberAccountChargeDefault.ToPagedList(pageNumber, pageSize));
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

                    memberAccountCharge = from a in db.MemberAccountCharge
                                           join acc in db.MemberAccount on a.MemberAccountId equals acc.MemberAccountId
                                           join mem in db.Members on acc.MemberId equals mem.Id
                                           where (mem.FirstName + " " + mem.LastName).Contains(searchString)
                                           select a;

                    var memberAccountChargeTotal = memberAccountCharge.Sum(d => d.Amount);
                    ViewBag.memberAccountChargeTotal = memberAccountChargeTotal;
                }


                switch (sortOrder)
                {
                    //case "fullname_desc":
                    //    memberAccountCharge = memberAccountCharge.OrderByDescending(m => m.MemberId.FirstName);
                    //    break;
                    //case "fullname":
                    //    memberAccountCharge = memberAccountCharge.OrderBy(m => m.MemberId.FirstName);
                    //    break;
                    default:
                        memberAccountCharge = memberAccountCharge.OrderByDescending(m => m.ChargeDate);
                        break;
                }
                return View(memberAccountCharge.ToPagedList(pageNumber, pageSize));
            }
        }
    
        public JsonResult AutoCompleteAccount(string term)
        {

            var customers = (from member in db.Members
                                join memAccount in db.MemberAccount on member.Id equals memAccount.MemberId
                             where (member.FirstName + " " + member.LastName + " ( ** Acc:" + memAccount.MemberAccountId + ")").Contains(term)
                             select new
                             {
                                 label = (member.FirstName + " " + member.LastName + " ( ** Acc:" + memAccount.MemberAccountId + ")"),
                                 val = memAccount.MemberAccountId
                             }).ToList();

            return Json(customers);

        }



    }
}

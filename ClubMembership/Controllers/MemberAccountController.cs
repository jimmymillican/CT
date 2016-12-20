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

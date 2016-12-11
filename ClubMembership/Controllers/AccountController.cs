using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClubMembership.Models;
using ClubMembership.DAL;

namespace ClubMembership.Controllers
{
    public class AccountController : Controller
    {
        private MembershipContext db = new MembershipContext();

        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(UserAccount account)
        {
            if(ModelState.IsValid)
            {
                using (db)
                {
                    db.UserAccounts.Add(account);
                    db.SaveChanges();
                }
                ModelState.Clear();
                ViewBag.Message = account.Username + " successfully registered.";
            }
            return View();
        }

        public ActionResult Login()
        {
            if (Session["UserId"] != null)
            {
                return RedirectToAction("LoggedIn");
            }
            return View();
        }

        [HttpPost]
        public ActionResult Login(UserAccount user)
        {
            using(db)
            {
                var usr = db.UserAccounts.FirstOrDefault(u => u.Username == user.Username && u.Password == user.Password);
                if(usr != null)
                {
                    Session["UserId"] = usr.UserId.ToString();
                    Session["Username"] = usr.Username.ToString();
                    return RedirectToAction("LoggedIn");
                }
                else
                {
                    ModelState.AddModelError("", "Username or password is wrong.");
                }
            }
            return View();
        }

        public ActionResult LoggedIn()
        {
            if (Session["UserId"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        public ActionResult LogOut()
        {
            Session.Clear();
            return View();
        }
    }
}
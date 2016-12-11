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
    public static class Extension
    {
        public static bool IsNumeric(this string s)
        {
            float output;
            return float.TryParse(s, out output);
        }
    }

    public class MemberController : Controller
    {

  

        private MembershipContext db = new MembershipContext();

        // GET: Member
        public ActionResult Index(string sortOrder, string searchString, string searchId ,string currentFilter, int? page, string type, bool topCondition = false)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            int pageSize = 10;
            int pageNumber = (page ?? 1);

            if (topCondition == true)
            {
                var topMembers = db.Members.ToList();
                foreach (var member in topMembers)
                {
                    foreach (var campaign in member.Campaigns)
                    {
                        member.Points += campaign.Level;
                    }
                }
                if(type == "OffPeak")
                {
                    var OffPeakMembers = (from m in topMembers where  m.Deleted == false && m.MemberType == MemberType.OffPeak && m.Expired == false select m);//.OrderByDescending(m => m.Points).Take(5);
                    return View(OffPeakMembers.ToPagedList(pageNumber, pageSize));
                }
                else if (type == "Peak")
                {
                    var PeakMembers = (from m in topMembers where m.Deleted == false && m.MemberType == MemberType.Peak && m.Expired == false select m);//.OrderByDescending(m => m.Points).Take(5);
                    return View(PeakMembers.ToPagedList(pageNumber, pageSize));
                }
                else if (type == "Expired")
                {
                    var ExpiredMembers = (from m in topMembers where m.Deleted == false && m.Expired == true  select m);//.OrderByDescending(m => m.Points).Take(5);
                    return View(ExpiredMembers.ToPagedList(pageNumber, pageSize));
                }
                else
                {
                    var topFive = (from m in topMembers where m.Deleted == false && m.Expired == false select m ).OrderByDescending(m => m.Points).Take(5);
                    return View(topFive.ToPagedList(pageNumber, pageSize));
                }   
            }
            else 
            {
                if (searchString != null )
                {
                    page = 1;
                }
                else
                {
                    searchString = currentFilter;
                }

                ViewBag.CurrentFilter = searchString;

                var members = from m in db.Members where m.Deleted == false select m;
                foreach (var member in members.ToList())
                {
                    foreach (var campaign in member.Campaigns)
                    {
                        member.Points += campaign.Level;
                    }
                }
                if (!String.IsNullOrEmpty(searchString))
                {
                    if (Extension.IsNumeric(searchString))
                    {
                        int idValue;
                        int.TryParse(searchString.TrimStart(new Char[] { '0' }), out idValue);
                        members = members.Where(m => m.Id == idValue);
                    }
                    else
                    {
                        members = members.Where(m => m.LastName.Contains(searchString)
                                                     || m.FirstName.Contains(searchString));
                    }

                }
                switch (sortOrder)
                {
                    case "name_desc":
                        members = members.OrderByDescending(m => m.LastName);
                        break;
                    case "Date":
                        members = members.OrderBy(m => m.MembershipDate);
                        break;
                    case "date_desc":
                        members = members.OrderByDescending(m => m.MembershipDate);
                        break;
                    default:
                        members = members.OrderBy(m => m.LastName);
                        break;
                }
                    return View(members.ToPagedList(pageNumber, pageSize));
             }      
        }

        // GET: Member/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Member member = db.Members.Find(id);
            foreach(var campaign in member.Campaigns)
            {
                member.Points += campaign.Level;
            }
            if (member == null)
            {
                return HttpNotFound();
            }
            return View(member);
        }

        // GET: Member/RegisterStartTime/5
        public ActionResult RegisterStartTime(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
 
            return RedirectToAction("Create", "TimeSheet", new {  id });
        }

        // GET: Member/Create
        public ActionResult UpdatePhoto(int? memberid)
        { 
            return RedirectToAction("Upload", "Avatar", new { id = memberid });
        }

        // GET: Member/Create
        public ActionResult Create()
        {
            var member = new Member();
            member.Campaigns = new List<Campaign>();
            PopulateAddedCampaigns(member);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,LastName,FirstName,MembershipDate,MemberType,Gender,DateOfBirth,Address1,PostCode,Telephone1,Telephone2")] Member member, string[] selectedCampaigns)

        {
            if(selectedCampaigns != null)
            {
                member.Campaigns = new List<Campaign>();
                foreach( var campaign in selectedCampaigns)
                {
                    var campaignToAdd = db.Campaigns.Find(int.Parse(campaign));
                    member.Campaigns.Add(campaignToAdd);
                }
            }

            if(ModelState.IsValid)
            {
                db.Members.Add(member);
                db.SaveChanges();
                int rtnId = member.Id;
                return RedirectToAction("Upload", "Avatar", new { id = rtnId });
            }

            PopulateAddedCampaigns(member);
            return View(member);
        }

        // GET: Member/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Member member = db.Members.Include(i => i.Campaigns).Where(i => i.Id == id).Single();
            PopulateAddedCampaigns(member);
            if (member == null)
            {
                return HttpNotFound();
            }
            ViewBag.imagePath =  member.Id + ".jpg?" + DateTime.UtcNow;
            ViewBag.memberid = member.Id;
            return View(member);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? id, string[] selectedCampaigns)
        {
            if( id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var memberToUpdate = db.Members.Include(i => i.Campaigns).Where(i => i.Id == id).Single();

            if(TryUpdateModel(memberToUpdate, "", new string[] {"LastName","FirstName","MembershipDate","MemberType","Gender","DateOfBirth","Address1","PostCode", "Telephone1", "Telephone2", "Expired", "ExpiryNotes" }))
            {
                try
                {
                    UpdateMemberCampaigns(selectedCampaigns, memberToUpdate);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch(RetryLimitExceededException)
                {
                    ModelState.AddModelError("", "Unable to save changes");
                }
            }
            PopulateAddedCampaigns(memberToUpdate);
            return View(memberToUpdate);
        }

        // GET: Member/Delete/5
        public ActionResult Delete(int? id, bool? saveChangesError=false)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Delete failed.";
            }
            Member member = db.Members.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            return View(member);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                Member member = db.Members.Find(id);
                member.Deleted = true;
                db.SaveChanges();
            }
            catch (DataException)
            {
                return RedirectToAction("Delete", new { id = id, saveChangesError = true });
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegisterStartTimeSubmit(int id)
        {
            //try
            //{
            //    Member member = db.Members.Find(id);
            //    db.Members.Remove(member);
            //    db.SaveChanges();
            //}
            //catch (DataException)
            //{
            //    return RedirectToAction("Delete", new { id = id, saveChangesError = true });
            //}
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private void PopulateAddedCampaigns(Member member)
        {
            var allCampaigns = db.Campaigns;
            var memberCampaigns = new HashSet<int>(member.Campaigns.Select(c => c.CampaignId));
            var viewModel = new List<AddedCampaigns>();

            foreach(var campaign in allCampaigns)
            {
                viewModel.Add(new AddedCampaigns
                {
                    CampaignId = campaign.CampaignId,
                    Title = campaign.Title,
                    Added = memberCampaigns.Contains(campaign.CampaignId)
                });
            }

            ViewBag.Campaigns = viewModel;
        }

        private void UpdateMemberCampaigns(string[] selectedCampaigns, Member memberToUpdate)
        {
            if(selectedCampaigns == null)
            {
                memberToUpdate.Campaigns = new List<Campaign>();
                return;
            }

            var selectedCampaignsHS = new HashSet<string>(selectedCampaigns);
            var memberCampaigns = new HashSet<int>(memberToUpdate.Campaigns.Select(c => c.CampaignId));

            foreach( var campaign in db.Campaigns)
            {
                if(selectedCampaignsHS.Contains(campaign.CampaignId.ToString()))
                {
                    if(!memberCampaigns.Contains(campaign.CampaignId))
                    {
                        memberToUpdate.Campaigns.Add(campaign);
                    }
                }
                else
                {
                    if(memberCampaigns.Contains(campaign.CampaignId))
                    {
                        memberToUpdate.Campaigns.Remove(campaign);
                    }
                }
            }
        }
    }
}

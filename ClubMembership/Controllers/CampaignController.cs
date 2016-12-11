using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using PagedList;
using ClubMembership.DAL;
using ClubMembership.Models;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Mvc;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Sample.MVC4;
using Google.Apis.Services;
using Google.Apis.Util.Store;

namespace ClubMembership.Controllers
{
    public class CampaignController : Controller
    {
        private MembershipContext db = new MembershipContext();

        // If modifying these scopes, delete your previously saved credentials
        // at ~/.credentials/calendar-dotnet-quickstart.json
        static string[] Scopes = { CalendarService.Scope.Calendar };
        static string ApplicationName = "Google Calendar API .NET Quickstart";

        // GET: Campaign
        public ActionResult Index(string sortOrder, string searchString, string currentFilter, int? page)
        {

           
            ///* START OF GOOOGLE TEST*/
            //UserCredential credential;
             
            //    ClientSecrets secrets = new ClientSecrets()
            //    {
            //        ClientId = "205742129432-m0p3vlqb36colf53q370fumgt5ramp6c.apps.googleusercontent.com",
            //        ClientSecret = "MQ1Zi9o5xiwp2ufrYBWl_cu6"
            //    };

            //    credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
            //        secrets,
            //        Scopes,
            //        "user",
            //        CancellationToken.None,
            //        new FileDataStore("C:/Users/jimmy.millican/Development/PersonalSites/ClubMembership-master/ClubMembership/ClubMembership/credentials/calendar-dotnet-quickstart.json", true)).Result;
 
           

            //// Create Google Calendar API service.
            //var service = new CalendarService(new BaseClientService.Initializer()
            //{
            //    HttpClientInitializer = credential,
            //    ApplicationName = ApplicationName,
            //});

            //// Define parameters of request.
            //EventsResource.ListRequest request = service.Events.List("primary");
            //request.TimeMin = DateTime.Now;
            //request.ShowDeleted = false;
            //request.SingleEvents = true;
            //request.MaxResults = 10;
            //request.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;

            //// List events.
            //Events events = request.Execute();
            //Console.WriteLine("Upcoming events:");
            //if (events.Items != null && events.Items.Count > 0)
            //{
            //    foreach (var eventItem in events.Items)
            //    {
            //        string when = eventItem.Start.DateTime.ToString();
            //        if (String.IsNullOrEmpty(when))
            //        {
            //            when = eventItem.Start.Date;
            //        }
            //        //Console.WriteLine("{0} ({1})", eventItem.Summary, when);
            //    }
            //}
            //else
            //{
            //   // Console.WriteLine("No upcoming events found.");
            //}
            //DateTime startDateTime = new DateTime();
            //startDateTime = DateTime.Now;
            //EventDateTime EventStartDTime = new EventDateTime();
            //EventStartDTime.DateTime = startDateTime;
            //EventStartDTime.TimeZone= "America/Los_Angeles";

            //EventDateTime EventEndtDTime = new EventDateTime();
            //EventEndtDTime.DateTime = startDateTime; 
            //EventEndtDTime.TimeZone = "Asia/Karachi";

            //var myEvent = new Event
            //{
            //    Summary = "Google Calendar Api test by jimmy ",
            //    Location = "Liverpool",
            //    Start = EventStartDTime,
            //    End =  EventEndtDTime,
            //    Recurrence = new String[] { "RRULE:FREQ=WEEKLY;BYDAY=MO" },
            //    Attendees = new List<EventAttendee>
            //    {
            //    new EventAttendee { Email = "jmillican@gmail.com"}
            //    },
            //};

            //var recurringEvent = service.Events.Insert(myEvent, "primary");
            //recurringEvent.SendNotifications = true;
            //recurringEvent.Execute();

            ///* END OF GOOOGLE TEST*/


            ViewBag.CurrentSort = sortOrder;
            ViewBag.TitleSortParm = String.IsNullOrEmpty(sortOrder) ? "title_desc" : "";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var campaigns = from s in db.Campaigns select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                campaigns = campaigns.Where(s => s.Title.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "title_desc":
                    campaigns = campaigns.OrderByDescending(s => s.Title);
                    break;
                default:
                    campaigns = campaigns.OrderBy(s => s.Title);
                    break;
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(campaigns.ToPagedList(pageNumber, pageSize));
        }

        // GET: Campaign/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Campaign campaign = db.Campaigns.Find(id);
            if (campaign == null)
            {
                return HttpNotFound();
            }
            return View(campaign);
        }

        // GET: Campaign/Create
        public ActionResult Create()
        {
            ViewBag.EditionId = new SelectList(db.Editions, "EditionId", "Title");
            return View();
        }

        // POST: Campaign/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CampaignId,Title,EditionId,Level")] Campaign campaign)
        {
            if (ModelState.IsValid)
            {
                db.Campaigns.Add(campaign);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EditionId = new SelectList(db.Editions, "EditionId", "Title", campaign.EditionId);
            return View(campaign);
        }

        // GET: Campaign/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Campaign campaign = db.Campaigns.Find(id);
            if (campaign == null)
            {
                return HttpNotFound();
            }
            ViewBag.EditionId = new SelectList(db.Editions, "EditionId", "Title", campaign.EditionId);
            return View(campaign);
        }

        // POST: Campaign/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CampaignId,Title,EditionId,Level")] Campaign campaign)
        {
            if (ModelState.IsValid)
            {
                db.Entry(campaign).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EditionId = new SelectList(db.Editions, "EditionId", "Title", campaign.EditionId);
            return View(campaign);
        }

        // GET: Campaign/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Campaign campaign = db.Campaigns.Find(id);
            if (campaign == null)
            {
                return HttpNotFound();
            }
            return View(campaign);
        }

        // POST: Campaign/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Campaign campaign = db.Campaigns.Find(id);
            db.Campaigns.Remove(campaign);
            db.SaveChanges();
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
    }
}

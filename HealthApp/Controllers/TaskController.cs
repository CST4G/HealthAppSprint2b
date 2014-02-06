using healthApp.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace healthApp.Controllers
{
    public class TaskController : Controller
    {
        private TaskDBContext db = new TaskDBContext();

        // GET: /Task/
        public ActionResult Index()
        {
            return View(db.Tasks.ToList());
        }

        //populate today's Shedule based on Tasks
        public ActionResult GenerateSched()
        {
            DateTime date = DateTime.Today; //date will be set to today
            String[] days = { "Sun", "Mon", "Tue", "Wed", "Thr", "Fri", "Sat" };
            String dow = days[(int)date.DayOfWeek]; //day of week
            int dom = date.Day;

            var tasks = from s in db.Tasks
                        where
                            // CONDITIONS: these will depend on the Calendar format that we choose
                            //every day, end date defined
                            (s.dtStart <= date && s.dtEnd >= date && s.freq.Equals("daily") && DbFunctions.DiffDays(date, s.dtStart) % s.interval == 0) //s.interval == 1 && 
                            ||           //every day, count defined
                            (s.dtStart <= date && DbFunctions.AddDays(s.dtStart, s.count) >= date && s.freq.Equals("daily") && DbFunctions.DiffDays(date, s.dtStart) % s.interval == 0)
                            ||          //every week, end date defined 
                            (s.dtStart <= date && s.dtEnd >= date && s.freq.Equals("weekly") && s.byDay.Contains(dow)
                                    && (DbFunctions.DiffDays(date, DbFunctions.AddDays(s.dtStart, -s.dtStartWD)) / 7) % s.interval == 0)
                            ||          //every week, count defined
                            (s.dtStart <= date && DbFunctions.AddDays(s.dtStart, s.count * 7) >= date && s.freq.Equals("weekly") && s.byDay.Contains(dow)
                                    && (DbFunctions.DiffDays(date, DbFunctions.AddDays(s.dtStart, -s.dtStartWD)) / 7) % s.interval == 0)
                            ||          //every month, end date defined        
                            (s.dtStart <= date && s.freq.Equals("monthly") && DbFunctions.AddMonths(s.dtStart, s.count) >= date
                                     && s.byMonthDay == dom && DbFunctions.DiffMonths(date, s.dtStart) % s.interval == 0)
                            ||          //every month, count defined        
                            (s.dtStart <= date && s.freq.Equals("monthly") && s.dtEnd >= date
                                     && s.byMonthDay == dom && DbFunctions.DiffMonths(date, s.dtStart) % s.interval == 0)
                        select s;

            Schedule sc = new Schedule();
            //populate schedule with new records
            foreach (var item in tasks)
            {
                sc.taskID = item.ID;
                sc.PatientID = item.PatientID;
                sc.Task = item.Task;
                sc.RoomNo = item.RoomNo;
                sc.duration = item.duration;
                sc.tDate = DateTime.Today + item.dtStart.TimeOfDay;
                db.Sched.Add(sc);
            }

            db.SaveChanges();
            return View(db.Tasks.ToList());
        }

        // GET: /Task/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tasks tasks = db.Tasks.Find(id);
            if (tasks == null)
            {
                return HttpNotFound();
            }
            return View(tasks);
        }

        // GET: /Task/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Task/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create([Bind(Include = "ID,PatientID,RoomNo,Task,duration,dtStart,dtEnd,freq,interval,count,byDay,byMonthDay")] Tasks tasks)
        {
            if (ModelState.IsValid)
            {
                db.Tasks.Add(tasks);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tasks);
        }

        // GET: /Task/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tasks tasks = db.Tasks.Find(id);
            if (tasks == null)
            {
                return HttpNotFound();
            }
            return View(tasks);
        }

        // POST: /Task/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Edit([Bind(Include = "ID,PatientID,RoomNo,Task,duration,dtStart,dtEnd,freq,interval,count,byDay,byMonthDay")] Tasks tasks)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tasks).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tasks);
        }

        // GET: /Task/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tasks tasks = db.Tasks.Find(id);
            if (tasks == null)
            {
                return HttpNotFound();
            }
            return View(tasks);
        }

        // POST: /Task/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Tasks tasks = db.Tasks.Find(id);
            db.Tasks.Remove(tasks);
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

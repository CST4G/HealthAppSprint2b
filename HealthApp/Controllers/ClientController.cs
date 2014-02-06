using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using healthApp.Models;

namespace healthApp.Controllers
{
    public class ClientController : Controller
    {
        private ClientDBContext db = new ClientDBContext();

        // GET: /Clients/
        public ActionResult Index(int? id)
        {
            @ViewBag.Sidebar = "Client";
            if (hasAccess())
            {
                if (id.HasValue)
                {
                    return View(Sort(id));
                }
                else 
                {
                    return View(db.Client.ToList());
                }
            }
            return RedirectToAction("Index", "Home");
        }
   
        public bool hasAccess()
        { 
            return ((Session["UserProfile"] != null) && (
            ((UserProfileObj)Session["UserProfile"]).accType == "sysadmin" ||
            ((UserProfileObj)Session["UserProfile"]).accType ==  "admin"));

        }
         [HttpGet]
        public IEnumerable<Client> Sort(int? id)
        {
            
            switch (id)
            {
                case 1:
                    return  db.Client.ToList().OrderBy(profile => profile.ClientFirstName);
                case 2:
                    return  db.Client.ToList().OrderBy(profile =>profile.ClientLastName);
                case 3:
                    return db.Client.ToList().OrderBy(profile => profile.ClientBedNum);
                case 4:
                    return db.Client.ToList().OrderBy(profile => profile.ClientDOB);
                default:
                    return db.Client.ToList();
            }
        }

        public ActionResult UploadPicture(int? id)
        {
            if (hasAccess())
            {
                if (id == null)
                {
                    return View("Index");
                }
                Client client = db.Client.Find(id);
                if (client == null)
                {
                    return View("Index");
                }
                return View(client);
            }
            return RedirectToAction("Index");
        }

        ActionResult x;
        [HttpGet]
        [AllowAnonymous]
        public ActionResult ViewImage(int id)
        {

            Client client = db.Client.SingleOrDefault(profile => profile.ClientID == id);
            if (client.ClientPicture == null)
            {
                return null;
            }
            byte[] buffer = client.ClientPicture;
            x = File(buffer, "image/jpg", string.Format("{0}", id));
            return x;
        }

        public ActionResult Upload(HttpPostedFileBase file, int id)
        {
            if (hasAccess())
            {
                if (file == null)
                {
                    return RedirectToAction("Index");
                }
                String name = file.FileName;

                System.Drawing.Image img = System.Drawing.Image.FromStream(file.InputStream);
                TypeConverter converter = TypeDescriptor.GetConverter(typeof(Bitmap));
                byte[] imgArray = (byte[])converter.ConvertTo(img, typeof(byte[]));

                Client client = db.Client.Find(id);
                //db.Clients.Remove(client);
                //db.SaveChanges();
                client.ClientPicture = imgArray;
                //db.Clients.Add(client);
                db.SaveChanges();
                return RedirectToAction("Index", "Client");
            }
            return RedirectToAction("Index", "Client");
            // saving to the database  call should go here 
            // return View(client);
        }

        // GET: /Clients/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Client client = db.Client.Find(id);
            if (client == null)
            {
                return RedirectToAction("Index");
            }
            return View(client);
        }

        // GET: /Clients/Create
        public ActionResult Create()
        {
            if (hasAccess())
            {
                return View();
            }
            return RedirectToAction("Index", "Client");
        }

        // POST: /Clients/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ClientID,ClientFirstName,ClientLastName,ClientMarital,ClientDOB,ClientHealthNum,ClientGender,ClientBedNum,ClientFamilyDoc,ClientPicture")] Client client)
        {
            if (hasAccess())
            {
                if (ModelState.IsValid)
                {
                    db.Client.Add(client);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(client);
            }
            return RedirectToAction("Index", "Client");
        }

        // GET: /Clients/Edit/5
        public ActionResult Edit(int? id)
        {
            if (hasAccess())
            {
                if (id == null)
                {
                    return RedirectToAction("Index");
                }
                Client client = db.Client.Find(id);
                if (client == null)
                {
                    return RedirectToAction("Index");
                }
                return View(client);
            }
            return RedirectToAction("Index", "Client");
        }

        // POST: /Clients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ClientID,ClientFirstName,ClientLastName,ClientMarital,ClientDOB,ClientHealthNum,ClientGender,ClientBedNum,ClientFamilyDoc,ClientPicture")] Client client)
        {
            if (hasAccess())
            {
                if (ModelState.IsValid)
                {
                    db.Entry(client).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(client);
            }
            return RedirectToAction("Index", "Client");
        }

        // GET: /Clients/Delete/5
        public ActionResult Delete(int? id)
        {
            if (hasAccess())
            {
                if (id == null)
                {
                    return RedirectToAction("Index");
                }
                Client client = db.Client.Find(id);
                if (client == null)
                {
                    return RedirectToAction("Index");
                }
                return View(client);
            }
            return RedirectToAction("Index", "Client");
        }

        // POST: /Clients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (hasAccess())
            {
                Client client = db.Client.Find(id);
                db.Client.Remove(client);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index", "Client");
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
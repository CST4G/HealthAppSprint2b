using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Web;
using healthApp.Models;

using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using System.Web.Security;

namespace healthApp.Controllers
{
    public class LoginController : Controller
    {
        AccountsDBContext db = new AccountsDBContext();

        public bool hasAccess()
        {
            return ((Session["UserProfile"] != null) &&
            ((UserProfileObj)Session["UserProfile"]).accType == "sysadmin");
          
        }
        // Index if system admin gives access to all users otherwise redirecto to Login
        // GET: /Login/
        public ActionResult Index()
        { 
            if (hasAccess())
            {
                return View(db.Accounts.ToList());
            }else
            {
                return RedirectToAction("Login","Login");
            }
        }

        // GET: /Login/Login
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }


        // POST: /Login/Login
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(Credentials model, string returnUrl)
        {

            if (Accounts.IsValid(model.UserName, model.Password, db))
            {
                
                string accType = Accounts.findType(model.UserName, model.Password, db);
              

                var profileData = new UserProfileObj(model.UserName, accType);

                Session["UserProfile"] = profileData;
                FormsAuthentication.SetAuthCookie(model.UserName, true);

             
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }

            


            // If we got this far, something failed, redisplay form
            
        }




        //SIGN OUT
        //
        // POST: /Login/LogOff
        public ActionResult LogOff()
        {
            this.Session.Clear();
            return RedirectToAction("Index", "Home");
        }


     


        // GET: /Login/Details/5
        public ActionResult Details(int? id)
        {
            if (hasAccess())
            {
                if (id == null)
                {
                    return RedirectToAction("Index");

                }
                Accounts accounts = db.Accounts.Find(id);
                if (accounts == null)
                {
                    return RedirectToAction("Index");

                }

                return View(accounts);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // GET: /Login/Create
        public ActionResult Create()
        {
           if(hasAccess()){
               return View();
            }else{
                return RedirectToAction("Index", "Home");
            }
            
        }

        // POST: /Login/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ID,UserName,Password,fName,lName,acctType")] Credentials credential)
        {
            Accounts accounts = Accounts.createFromCredential(credential);

            if (hasAccess())
            {
                if (ModelState.IsValid)
                {
                    db.Accounts.Add(accounts);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // GET: /Login/Edit/5
        public ActionResult Edit(int? id)
        {
            if (hasAccess())
            {
                if (id == null)
                {
                    return RedirectToAction("Index");

                }
                Accounts accounts = db.Accounts.Find(id);
                if (accounts == null)
                {
                    return RedirectToAction("Index");

                }
                return View(accounts);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // POST: /Login/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,UserName,Password,fName,lName,acctType")] Credentials credential)
        {
            Accounts accounts = Accounts.createFromCredential(credential);
            if (hasAccess())
            {
                if (ModelState.IsValid)
                {
                    db.Entry(accounts).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(accounts);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // GET: /Login/Delete/5
        public ActionResult Delete(int? id)
        {

            if (hasAccess())
            {
                if (id == null)
                {
                    return RedirectToAction("Index");

                }
                Accounts account = db.Accounts.Find(id);
                if (account == null)
                {
                    return RedirectToAction("Index");

                }
                return View(account);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // POST: /Login/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (hasAccess())
            {
                Accounts accounts = db.Accounts.Find(id);
                db.Accounts.Remove(accounts);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
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

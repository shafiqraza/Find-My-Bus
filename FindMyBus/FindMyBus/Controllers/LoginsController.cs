using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FindMyBus.Models;
using System.Web.Security;

namespace FindMyBus.Controllers
{
    
    public class LoginsController : Controller
    {

        

        private BusModelContainer db = new BusModelContainer();


        [Authorize(Roles = "Admin")]
        // GET: Logins
        public ActionResult Index()
        {
            return View(db.Logins.ToList());
        }

        [Authorize(Roles = "Admin")]

        // GET: Logins/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Login login = db.Logins.Find(id);
            if (login == null)
            {
                return HttpNotFound();
            }
            return View(login);
        }

        [Authorize(Roles = "Admin")]

        // GET: Logins/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Logins/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FullName,UserName,Password,Contact")] Login login)
        {
            bool emailCheck = db.Logins.Any(x => x.UserName == login.UserName);
            if (emailCheck)
            {
                ModelState.AddModelError("", "This Email is already in used try different one.");
                return View();
            }
            else
            {
                if (ModelState.IsValid)
                {
                    db.Logins.Add(login);
                    db.SaveChanges();
                    //how to get last inserted ID in ASP MVC
                    int lastId = db.Logins.Max(item => item.Id);

                    var defaultRole = new LoginRoles
                    {
                        LoginId = lastId,
                        Role = "Customer",
                    };

                    db.LoginRoles.Add(defaultRole);
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
            }

            return View(login);
        }

        // GET: Logins/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Login login = db.Logins.Find(id);
            if (login == null)
            {
                return HttpNotFound();
            }
            return View(login);
        }

        // POST: Logins/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FullName,UserName,Password,Contact")] Login login)
        {
            if (ModelState.IsValid)
            {
                db.Entry(login).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(login);
        }

        // GET: Logins/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Login login = db.Logins.Find(id);
            if (login == null)
            {
                return HttpNotFound();
            }
            return View(login);
        }

        // POST: Logins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            Login login = db.Logins.Find(id);
            db.Logins.Remove(login);
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

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Login formData)
        {
            bool formCheck = db.Logins.Any(x => x.UserName == formData.UserName && x.Password == formData.Password);
            if (formCheck)
            {
                var userRow = db.Logins.Where(x => x.UserName == formData.UserName && x.Password == formData.Password).FirstOrDefault();
                Session["currentUserId"] = userRow.Id;
                ViewData["currentUserEmail"] = userRow.UserName;
                FormsAuthentication.SetAuthCookie(formData.UserName,false);
                
                return Redirect("/Home/Index");
            }
            else
            {
                ModelState.AddModelError("","Invalid User Name Pawword");
                return View();
            }
            
        }
        public ActionResult Signup()
        {
            return View();

        }

        [HttpPost,ValidateAntiForgeryToken]
        public ActionResult Signup( Login formData)
        {
            bool emailCheck = db.Logins.Any(x => x.UserName == formData.UserName);
            if (emailCheck)
            {
                ModelState.AddModelError("", "This Email is already in used try different one.");
                return View();
            }
            else
            {
                if (ModelState.IsValid)
                {
                    db.Logins.Add(formData);
                    db.SaveChanges();
                    //how to get last inserted ID in ASP MVC
                    int lastId = db.Logins.Max(item => item.Id);

                    var defaultRole = new LoginRoles
                    {
                        LoginId = lastId,
                        Role = "Customer",
                    };

                    db.LoginRoles.Add(defaultRole);
                    db.SaveChanges();

                    return Redirect("/Home/Index");
                }
                else
                {
                    return View(formData);
                }
                
            }
             
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return Redirect("/Home/Index");
        }

        [Authorize]
        public ActionResult ChangePassword()
        {
            return View();
        }
        [Authorize]
        [HttpPost]
        public ActionResult ChangePassword( int id, string password,string confirmPassword)
        {
            if(password == confirmPassword)
            {
                var passwordChanged = db.Logins.Where(x => x.Id == id).FirstOrDefault();
                passwordChanged.Password = password;
                db.SaveChanges();
                return View();
            }
            else
            {
                //ModelState.AddModelError("", "New Password and Confirm Password NOT matched !");
                ViewBag.error = "New Password and Confirm Password NOT matched !";
                return View();
            }
            
            
        }
    }
}

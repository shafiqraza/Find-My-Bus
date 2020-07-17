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
    [Authorize(Roles ="Admin")]
    public class LoginRolesController : Controller
    {
        private BusModelContainer db = new BusModelContainer();


        

        // GET: LoginRoles
        public ActionResult Index()
        {
            var loginRoles = db.LoginRoles.Include(l => l.Login);
            return View(loginRoles.ToList());
        }

        // GET: LoginRoles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoginRoles loginRoles = db.LoginRoles.Find(id);
            if (loginRoles == null)
            {
                return HttpNotFound();
            }
            return View(loginRoles);
        }

        // GET: LoginRoles/Create
        public ActionResult Create()
        {
            ViewBag.LoginId = new SelectList(db.Logins, "Id", "FullName");
            return View();
        }

        // POST: LoginRoles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Role,LoginId")] LoginRoles loginRoles)
        {
            if (ModelState.IsValid)
            {
                db.LoginRoles.Add(loginRoles);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.LoginId = new SelectList(db.Logins, "Id", "FullName", loginRoles.LoginId);
            return View(loginRoles);
        }

        // GET: LoginRoles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoginRoles loginRoles = db.LoginRoles.Find(id);
            if (loginRoles == null)
            {
                return HttpNotFound();
            }
            ViewBag.LoginId = new SelectList(db.Logins, "Id", "FullName", loginRoles.LoginId);
            return View(loginRoles);
        }

        // POST: LoginRoles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Role,LoginId")] LoginRoles loginRoles)
        {
            if (ModelState.IsValid)
            {
                db.Entry(loginRoles).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LoginId = new SelectList(db.Logins, "Id", "FullName", loginRoles.LoginId);
            return View(loginRoles);
        }

        // GET: LoginRoles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoginRoles loginRoles = db.LoginRoles.Find(id);
            if (loginRoles == null)
            {
                return HttpNotFound();
            }
            return View(loginRoles);
        }

        // POST: LoginRoles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LoginRoles loginRoles = db.LoginRoles.Find(id);
            db.LoginRoles.Remove(loginRoles);
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

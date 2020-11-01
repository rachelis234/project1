using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using secondhand;

namespace secondhand.Controllers
{
    public class areasController : Controller
    {
        private rachelis_secondHand1Entities db = new rachelis_secondHand1Entities();

        // GET: areas
        public ActionResult Index()
        {
            var areas = db.areas.Include(a => a.areas2);
            return View(areas.ToList());
        }

        // GET: areas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            areas areas = db.areas.Find(id);
            if (areas == null)
            {
                return HttpNotFound();
            }
            return View(areas);
        }

        // GET: areas/Create
        public ActionResult Create()
        {
            ViewBag.parentId = new SelectList(db.areas, "Id", "name");
            return View();
        }

        // POST: areas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,name,C_type,parentId")] areas areas)
        {
            if (ModelState.IsValid)
            {
                db.areas.Add(areas);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.parentId = new SelectList(db.areas, "Id", "name", areas.parentId);
            return View(areas);
        }

        // GET: areas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            areas areas = db.areas.Find(id);
            if (areas == null)
            {
                return HttpNotFound();
            }
            ViewBag.parentId = new SelectList(db.areas, "Id", "name", areas.parentId);
            return View(areas);
        }

        // POST: areas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,name,C_type,parentId")] areas areas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(areas).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.parentId = new SelectList(db.areas, "Id", "name", areas.parentId);
            return View(areas);
        }

        // GET: areas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            areas areas = db.areas.Find(id);
            if (areas == null)
            {
                return HttpNotFound();
            }
            return View(areas);
        }

        // POST: areas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            areas areas = db.areas.Find(id);
            db.areas.Remove(areas);
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

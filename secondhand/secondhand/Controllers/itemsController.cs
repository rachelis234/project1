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
    public class itemsController : Controller
    {
        private rachelis_secondHand1Entities db = new rachelis_secondHand1Entities();

        // GET: items
        public ActionResult Index()
        {
            var items = db.items.Include(i => i.areas).Include(i => i.subCategory);
            return View(items.ToList());
        }
        public ActionResult ManagerView()
        {
            return View(Session["user"]);
        }
        public ActionResult UserItems()
        {
            users u = (users)Session["user"];
            var items = db.items.Where(i => i.ueserId ==u.id).ToList();
            return View(items);
        }
        [HttpPost]
        public PartialViewResult Search(int categorySelect, int subCategorySelect, int areasSelect, int citiesSelect, int fromPrice, int toPrice, string searchText)
        {
            //int subCategory = Convert.ToInt32(subCategorySelect);
            //int city = Convert.ToInt32(citiesSelect);
            int DefToPrice = toPrice == 0 ? Int32.MaxValue : toPrice;            
                    var items = db.items.Where(i => i.subCategoryId == subCategorySelect &&
                                          ( i.areaId == citiesSelect || citiesSelect == 407 )
                                          && i.price > fromPrice && i.price < toPrice && i.ShortDescription.Contains(searchText));
                    return PartialView(items.ToList());
        
        }

        // GET: items/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            items items = db.items.Find(id);
            if (items == null)
            {
                return HttpNotFound();
            }
            return View(items);
        }

        // GET: items/Create
        public ActionResult Create()
        {
            ViewBag.vsubCat = db.subCategory;
            ViewBag.vcities = db.areas.Where(a => a.C_type == 1);
            return View();
        }

        // POST: items/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CategoryId,Id,subCategoryId,ShortDescription,img,price,publicationDate,areaId,ueserId")] items items)
        {
            if (ModelState.IsValid)
            {
                db.items.Add(items);
                db.SaveChanges();               
            }
            ViewBag.areaId = new SelectList(db.areas, "Id", "name", items.areaId);
            ViewBag.subCategoryId = new SelectList(db.subCategory, "Id", "name", items.subCategoryId);
            int id = int.Parse(Session["id"].ToString());
            return View("../Views/users/UserItems", db.items.Where(t => t.ueserId == id).ToList());
        }

        // GET: items/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            items items = db.items.Find(id);
            if (items == null)
            {
                return HttpNotFound();
            }
            ViewBag.areaId = new SelectList(db.areas, "Id", "name", items.areaId);
            ViewBag.subCategoryId = new SelectList(db.subCategory, "Id", "name", items.subCategoryId);
            return View(items);
        }

        // POST: items/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CategoryId,Id,subCategoryId,ShortDescription,img,price,publicationDate,areaId,ueserId")] items items)
        {
            if (ModelState.IsValid)
            {
                db.Entry(items).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.areaId = new SelectList(db.areas, "Id", "name", items.areaId);
            ViewBag.subCategoryId = new SelectList(db.subCategory, "Id", "name", items.subCategoryId);
            return View(items);
        }

        // GET: items/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            items items = db.items.Find(id);
            if (items == null)
            {
                return HttpNotFound();
            }
            return View(items);
        }

        // POST: items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            items items = db.items.Find(id);
            db.items.Remove(items);
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

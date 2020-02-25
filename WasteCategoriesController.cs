using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CleanApp2018.Models;

namespace CleanApp2018.Controllers
{
    public class WasteCategoriesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: WasteCategories
        public async Task<ActionResult> Index()
        {
            return View(await db.WasteCategory.ToListAsync());
        }

        // GET: WasteCategories/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WasteCategory wasteCategory = await db.WasteCategory.FindAsync(id);
            if (wasteCategory == null)
            {
                return HttpNotFound();
            }
            return View(wasteCategory);
        }

        // GET: WasteCategories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: WasteCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "wCategoryId,wName,pricePerKg")] WasteCategory wasteCategory)
        {
            if (ModelState.IsValid)
            {
                if (wasteCategory.pricePerKg < 0)
                {
                    ModelState.AddModelError("", "You Can Not Enter A Value Less Than Zero/ Negetive");
                }
                else
                {
                    db.WasteCategory.Add(wasteCategory);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            return View(wasteCategory);
        }

        // GET: WasteCategories/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WasteCategory wasteCategory = await db.WasteCategory.FindAsync(id);
            if (wasteCategory == null)
            {
                return HttpNotFound();
            }
            return View(wasteCategory);
        }

        // POST: WasteCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "wCategoryId,wName,pricePerKg")] WasteCategory wasteCategory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(wasteCategory).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(wasteCategory);
        }

        // GET: WasteCategories/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WasteCategory wasteCategory = await db.WasteCategory.FindAsync(id);
            if (wasteCategory == null)
            {
                return HttpNotFound();
            }
            return View(wasteCategory);
        }

        // POST: WasteCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            WasteCategory wasteCategory = await db.WasteCategory.FindAsync(id);
            db.WasteCategory.Remove(wasteCategory);
            await db.SaveChangesAsync();
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

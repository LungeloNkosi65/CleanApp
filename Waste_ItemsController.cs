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
using System.IO;

namespace CleanApp2018.Controllers
{
    public class Waste_ItemsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Waste_Items
        public async Task<ActionResult> Index()
        {
            var waste_Items = db.Waste_Items.Include(w => w.WasteCategory);
            return View(await waste_Items.ToListAsync());
        }
        public async Task<ActionResult> Pictures()
        {
            var waste_Items = db.Waste_Items.Include(w => w.WasteCategory);
            return View(await waste_Items.ToListAsync());
        }



        // GET: Waste_Items/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Waste_Items waste_Items = await db.Waste_Items.FindAsync(id);
            if (waste_Items == null)
            {
                return HttpNotFound();
            }
            return View(waste_Items);
        }

        // GET: Waste_Items/Create
        public ActionResult Create()
        {
            ViewBag.wCategoryId = new SelectList(db.WasteCategory, "wCategoryId", "wName");
            return View();
        }

        // POST: Waste_Items/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "wItemId,wCategoryId,Iname,Image,ImageType,PricePerKg")] Waste_Items waste_Items,HttpPostedFileBase filelist)
        {
            if (ModelState.IsValid)
            {

                if (waste_Items.PricePerKg < 0)
                {
                    ModelState.AddModelError("", "You Can Not Enter A Value Less Than Zero/ Negetive");
                }
                else
                {
                    if (filelist != null && filelist.ContentLength > 0)
                    {
                        waste_Items.ImageType = Path.GetExtension(filelist.FileName);
                        waste_Items.Image = ConvertToBytes(filelist);
                    }
                    db.Waste_Items.Add(waste_Items);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            ViewBag.wCategoryId = new SelectList(db.WasteCategory, "wCategoryId", "wName", waste_Items.wCategoryId);
            return View(waste_Items);
        }
        public byte[] ConvertToBytes(HttpPostedFileBase file)
        {
            BinaryReader reader = new BinaryReader(file.InputStream);
            return reader.ReadBytes((int)file.ContentLength);


        }
        public FileStreamResult RenderImage(string id)
        {
            MemoryStream ms = null;

            var item = db.Waste_Items.FirstOrDefault(x => x.wItemId == id);
            if (item != null)
            {
                ms = new MemoryStream(item.Image);
            }
            return new FileStreamResult(ms, item.ImageType);
        }
        // GET: Waste_Items/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Waste_Items waste_Items = await db.Waste_Items.FindAsync(id);
            if (waste_Items == null)
            {
                return HttpNotFound();
            }
            ViewBag.wCategoryId = new SelectList(db.WasteCategory, "wCategoryId", "wName", waste_Items.wCategoryId);
            return View(waste_Items);
        }

        // POST: Waste_Items/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "wItemId,wCategoryId,Iname,PricePerKg")] Waste_Items waste_Items)
        {
            if (ModelState.IsValid)
            {
                db.Entry(waste_Items).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.wCategoryId = new SelectList(db.WasteCategory, "wCategoryId", "wName", waste_Items.wCategoryId);
            return View(waste_Items);
        }

        // GET: Waste_Items/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Waste_Items waste_Items = await db.Waste_Items.FindAsync(id);
            if (waste_Items == null)
            {
                return HttpNotFound();
            }
            return View(waste_Items);
        }

        // POST: Waste_Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            Waste_Items waste_Items = await db.Waste_Items.FindAsync(id);
            db.Waste_Items.Remove(waste_Items);
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

ausing System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CleanApp2018.Models;
using CleanApp2018.ViewModels;
namespace CleanApp2018.Controllers
{
    public class CalculatorController : Controller
    {
        // GET: Calculator
        ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            List<WasteCategory> CategoryList = db.WasteCategory.ToList();
            ViewBag.CategoryList = new SelectList(CategoryList, "wCategoryId", "wName");

            return View();
        }
        public JsonResult GetItemList(string wCategoryId)
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<Waste_Items> ItemList = db.Waste_Items.Where(x => x.wCategoryId == wCategoryId).ToList();
            return Json(ItemList, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]

        public ActionResult Index(CalculatorVM pointsCalculator)

        {
            if (pointsCalculator.ValueEntered < 0)
            {
                ModelState.AddModelError("", "You can not enter a value less than zoro /Negetive");

            }
            else
            {
                pointsCalculator.Calc();
                pointsCalculator.getName2();
                pointsCalculator.name2 = pointsCalculator.getName2();
                pointsCalculator.name = pointsCalculator.getName();
                pointsCalculator.AnswerW = pointsCalculator.w;
                pointsCalculator.AnswerP = pointsCalculator.p;
                pointsCalculator.AnswerR = pointsCalculator.r;
                //ViewBag.wCategoryId = new SelectList(db.WasteCategory, "wCategoryId", "wName", pointsCalculator.wCategoryId);
                //ViewBag.WItemID = new SelectList(db.Waste_Items,"wItemId", "Iname", pointsCalculator.WItemID);



                return View(pointsCalculator);
            }
                return View();

        }


    }
}
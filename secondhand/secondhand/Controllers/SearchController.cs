using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace secondhand.Controllers
{
    public class SearchController : Controller
    {
        // GET: Search
        rachelis_secondHand1Entities DB = new rachelis_secondHand1Entities();
        public ActionResult Index()
        {
            ViewBag.areas = DB.areas.Where(a=>a.C_type==2||a.C_type==0);
            ViewBag.cities = DB.areas.Where(c => c.C_type == 1);
            ViewBag.categories = DB.categories;
            ViewBag.items = DB.items;
            ViewBag.subCategory = DB.subCategory;
            ViewBag.users = DB.users;
            return View();
        }
        public JsonResult GetCities(int areasSelect)
        {
            List<SelectListItem> cities; 
            if (areasSelect != 400)
            {
                cities = DB.areas.Where(a => a.parentId == areasSelect).Select(s => new SelectListItem { Value = s.Id.ToString(), Text = s.name }).ToList();
                return Json(cities,JsonRequestBehavior.AllowGet);
            }
            cities = DB.areas.Where(c => c.C_type == 1).Select(s => new SelectListItem { Value = s.Id.ToString(), Text = s.name }).ToList();
            return  Json(cities, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetSubCategory(int categorySelect)
        {
            List<SelectListItem> subCategory;
            if (categorySelect != 261)
            {
                subCategory = DB.subCategory.Where(a => a.CategoryId == categorySelect).Select(s => new SelectListItem { Value = s.Id.ToString(), Text = s.name }).ToList();
                return Json(subCategory, JsonRequestBehavior.AllowGet);
            }
            subCategory = DB.subCategory.Select(s => new SelectListItem { Value = s.Id.ToString(), Text = s.name }).ToList();
            return Json(subCategory, JsonRequestBehavior.AllowGet);
        }
        
    }
}
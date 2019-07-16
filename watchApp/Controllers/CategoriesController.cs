using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using watchApp.Managers;
using watchApp.Models;

namespace watchApp.Controllers
{
    public class CategoriesController : Controller
    {
        private DbManager db = new DbManager();
        // GET: Categories
        public ActionResult Index()
        {
            var categories = db.GetCategories();
            return View(categories);
        }
        public ActionResult Create()
        {
            return View();

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Category category)
        {
            if (!ModelState.IsValid)
            {
                return View(category);
            }
            db.AddCategory(category);
            return RedirectToAction("Index");
        }
        public ActionResult Delete(string id)
        {
            Category category = db.GetCategory(id);
            if(category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            db.DeleteCategory(id);
            return RedirectToAction("Index");
        }
    }
}
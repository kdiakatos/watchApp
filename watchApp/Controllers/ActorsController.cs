using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using watchApp.Managers;
using watchApp.Models;

namespace watchApp.Controllers
{
    public class ActorsController : Controller
    {
        private DbManager db = new DbManager();
        // GET: Actors
        public ActionResult Index()
        {
            var actors = db.GetActors();
            ViewData["message"] = "we are on index page";
            return View(actors);
        }
        public ActionResult Create()
        {
            ViewBag.Message = "we are on create page!";
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Actor actor)
        {
            if (!ModelState.IsValid)
            {
                return View(actor);
            }

            db.AddActor(actor);
            TempData["notification-message"] = "actor iserted successfully!";
            TempData["notification-color"] = "success";
            Session["Actions"] = (int)Session["Actions"] + 1;
            return RedirectToAction("Index");


        }
        public ActionResult Edit (int id)
        {
            Actor actor = db.GetActor(id);
            if(actor == null)
            {
                return HttpNotFound();
            }
            return View(actor);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Actor actor)
        {
            if (!ModelState.IsValid)
            {
                return View(actor);
            }
            db.UpdateActor2(actor);
            
            return RedirectToAction("Index");

        }
        public ActionResult Delete(int id)
        {
            Actor actor = db.GetActor(id);
            if (actor == null)
            {
                return HttpNotFound();
            }
            return View(actor);
        }
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed (int id)
        {
            db.DeleteActor(id);
            TempData["notification-message"] = "Actor deleted successfully!";
            TempData["notification-color"] = "danger";
            Session["Actions"] = (int)Session["Actions"] + 1;
            return RedirectToAction("Index");
        }
        
    }
}
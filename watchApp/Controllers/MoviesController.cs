using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using watchApp.Managers;
using watchApp.Models;
using watchApp.ViewModels;

namespace watchApp.Controllers
{
    public class MoviesController : Controller
    {
        private DbManager db = new DbManager();
        // GET: Movies
        public ActionResult Index()
        {
            var movies = db.GetMovies();
            //prepei sto resurn View() na bazoume movies giati otan to trexomoyme 8a bgazei null sto models
            return View(movies);
        }

        public ActionResult Create()
        {
            var directors = db.GetDirectors().AsEnumerable();
            ViewBag.DirectorId = new SelectList(directors, "Id", "Name");
            var categories = db.GetCategories();
            ViewBag.Genre = new SelectList(categories, "Name", "Name");
            MovieViewModel vm = new MovieViewModel()
            {
                Movie = new Movie(),
                Actors = db.GetActors().Select(x => new SelectListItem()
                {
                    Value = x.Id.ToString(),
                    Text = x.Name
                })
            };
            return View(vm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MovieViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                var directors = db.GetDirectors().AsEnumerable();
                ViewBag.DirectorId = new SelectList(directors, "Id", "Name",vm.Movie.DirectorId);
                var categories = db.GetCategories();
                ViewBag.Genre = new SelectList(categories, "Name", "Name",vm.Movie.Genre);
                return View(vm);
            }
            db.AddMovie(vm.Movie, vm.SelectedActors);
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int id)
        {
            Movie movie = db.GetMovieFull(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            ViewBag.Genre = new SelectList(db.GetCategories(), "Name", "Name",movie.Genre);
            ViewBag.DirectorId = new SelectList(db.GetDirectors(), "Id", "Name",movie.DirectorId);
            MovieViewModel vm = new MovieViewModel()
            {
                Movie = movie,
                Actors = db.GetActors().Select(x => new SelectListItem()
                {
                    Value = x.Id.ToString(),
                    Text = x.Name
                })
            };
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MovieViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Genre = new SelectList(db.GetCategories(), "Name", "Name", vm.Movie.Genre);
                ViewBag.DirectorId = new SelectList(db.GetDirectors(), "Id", "Name", vm.Movie.DirectorId);
                return View(vm);
            }
            db.UpdateMovie(vm.Movie,vm.SelectedActors);
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int id)
        {
            Movie movie = db.GetMovieFull(id);
            if (movie== null)
            {
                return HttpNotFound();

            }
            return View(movie);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]

        public ActionResult DeleteConfirmed(int id)
        {
            db.DeleteMovie(id);
            return RedirectToAction("Index");
        }
    }
}




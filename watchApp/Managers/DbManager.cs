using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using watchApp.Models;
using System.Data.Entity;

namespace watchApp.Managers
{
    public class DbManager
    {

        public ICollection<Actor> GetActors()
        {
            ICollection<Actor> result;
            using (WatchDb db = new WatchDb())
            {
                result = db.Actors.ToList();
            }
            return result;
        }
         public Actor GetActor(int id)
        {
            Actor result;
            using (WatchDb db = new WatchDb())
            {
                result = db.Actors.Find(id);
            }
            return result;
        }
        public void AddActor(Actor actor)
        {
            using (WatchDb db = new WatchDb())
            {
                db.Actors.Add(actor);
                db.SaveChanges();
            }

        }
       
        public void UpdateActor(Actor actor)
        {
            using (WatchDb db = new WatchDb())
            {
                Actor db_actor = db.Actors.Find(actor.Id);
                db_actor.Name = actor.Name;
                db_actor.Age = actor.Age;
                db.SaveChanges();
            }

        }
        public void UpdateActor2(Actor actor)
        {
            using (WatchDb db = new WatchDb())
            {
                db.Actors.Attach(actor);
                db.Entry(actor).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public void DeleteActor(int id)
        {
            using (WatchDb db = new WatchDb())
            {
                Actor actor = db.Actors.Find(id);
                //1st way
                db.Actors.Remove(actor);
                //2nd way
                // db.Entry(actor).State = Entity.Deleted;
                db.SaveChanges();
            }
        }

        //director
        public ICollection<Director> GetDirectors()
        {
            ICollection<Director> result;
            using (WatchDb db = new WatchDb())
            {
                result= db.Directors.ToList();
            }
            return result;
        }
        public void AddDirector(Director director)
        {
            using (WatchDb db = new WatchDb())
            {
                db.Directors.Add(director);
                db.SaveChanges();
            }
        }
        public Director GetDirector(int id)
        {
            Director result;
            using (WatchDb db = new WatchDb())
            {
                result = db.Directors.Find(id);

            }
            return result;
        }
        public void UpdateDirector(Director director)
        {
            using (WatchDb db = new WatchDb())
            {
                Director db_director = db.Directors.Find(director.Id);
                db_director.Name = director.Name;
                db_director.Age = director.Age;
                db.SaveChanges();

            }
        }
        public void DeleteDirector(int id)
        {
            using (WatchDb db = new WatchDb())
            {
                Director director = db.Directors.Find(id);
                db.Directors.Remove(director);
                db.SaveChanges();
            }
        }

        // categories
        public ICollection<Category> GetCategories()
        {
            ICollection<Category> result;
            using (WatchDb db = new WatchDb())
            {
                result = db.Categories.ToList();
            }
            return result;
        }
        public Category GetCategory(string name)
        {
            Category result;
            using (WatchDb db = new WatchDb())
            {
                result = db.Categories.Find(name);
            }
            return result;

        }
        public void AddCategory(Category category)
        {
            using (WatchDb db = new WatchDb())
            {
                db.Categories.Add(category);
                db.SaveChanges();
            }
        }
        public void DeleteCategory(string name)
        {
            using (WatchDb db = new WatchDb())
            {
                Category category = db.Categories.Find(name);
                db.Categories.Remove(category);
                db.SaveChanges();

            }
        }

        public ICollection<Movie> GetMovies()
        {
            ICollection<Movie> result;
            using(WatchDb db = new WatchDb())
            {
                result = db.Movies.Include("Category").Include("Director").Include("Actors").ToList();
            }
            return result;
        }

        public void AddMovie(Movie movie,List<int> actorsIds)
        {
            using (WatchDb db = new WatchDb())
            {
                db.Movies.Add(movie);
                db.SaveChanges();
                foreach (var id in actorsIds)
                {
                    Actor actor = db.Actors.Find(id);
                    if(actor != null)
                    {
                        movie.Actors.Add(actor);
                    }
                }
                db.SaveChanges();
            }
        }

        public Movie GetMovie(int id)
        {
            Movie result;
            using(WatchDb db = new WatchDb())
            {
                result = db.Movies.Find(id);
            }
            return result;
        }

        public Movie GetMovieFull(int id)
        {
            Movie result;
            using (WatchDb db = new WatchDb())
            {
                result = db.Movies.Include("Category").Include("Director").Include("Actors").Where(x => x.Id == id).FirstOrDefault();
            }
            return result;
        }
       public void UpdateMovie(Movie movie, List<int> actorsIds)
        {
            using (WatchDb db = new WatchDb())
            {
                db.Movies.Attach(movie);
                db.Entry(movie).Collection("Actors").Load();
                movie.Actors.Clear();
                db.SaveChanges();
                foreach (int id in actorsIds)
                {
                    Actor actor = db.Actors.Find(id);
                    if (actor != null)
                    {
                        movie.Actors.Add(actor);
                    }
                }
                db.Entry(movie).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public void DeleteMovie(int id)
        {
            using (WatchDb db = new WatchDb())
            {
                Movie movie = db.Movies.Find(id);
                db.Movies.Remove(movie);
                db.SaveChanges();
            }
        }
        
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace watchApp.Models
{
    public class Movie
    {
        public Movie()
        {
            Actors = new HashSet<Actor>();
        }
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public int Year { get; set; }
        public bool Watched { get; set; }


        [ForeignKey("Category")]
        public string Genre { get; set; }

        public virtual Category Category { get; set; }
        [Display(Name ="DIrector")]

        public int DirectorId { get; set; }
        public virtual Director Director { get; set; }
        public virtual ICollection<Actor> Actors { get; set; }



    }
}
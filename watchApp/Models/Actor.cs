using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace watchApp.Models
{
    public class Actor
    {
        
        public int Id { get; set; }
        [Required (ErrorMessage ="vale kati!")]

        public string Name { get; set; }
        [Range(1,150, ErrorMessage ="vale logiko arithmo!")]
        public int Age { get; set; }
        public virtual ICollection<Movie> Movies { get; set; }
    }
}
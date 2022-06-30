using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace projekt.Models
{
    public class Movie
    {
        [Key]
        [Display(Name = "Id")]
        [Required]
        public int Id { get; set; }
        [Display(Name = "Nazwa")]
        [Required]
        public string Name { get; set; }
        [Display(Name = "Czas trwania")]
        [Required]
        public int RunningTime { get; set; }
        [Display(Name = "Zwiastun filmu")]
        public string Trailer { get; set; }
        [Display(Name = "Kategoria")]
        public IList<CategoryMovie> CategoryMovies { get; set; }
        [Display(Name = "Seans")]
        public ICollection<MovieShow> MovieShows { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace projekt.Models
{
    public class MovieShow
    {
        [Key]
        [Display(Name = "Id")]
        [Required]
        public int Id { get; set; }
        [Display(Name = "Sposób projekcji")]
        [Required]
        public string Format { get; set; }
        [Display(Name = "Film")]
        public int MovieId { get; set; }
        [Display(Name = "Film")]
        public Movie Movie { get; set; }
    }
}

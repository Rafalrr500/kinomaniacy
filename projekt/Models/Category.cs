using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace projekt.Models
{
    public class Category
    {
        [Key]
        [Display(Name = "Id")]
        [Required]
        public int Id { get; set; }
        [Display(Name = "Nazwa kategorii")]
        [Required]
        public string Name { get; set; }
        [Display(Name = "Filmy")]
        public IList<CategoryMovie> CategoryMovies { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace projekt.Models
{
    public class CategoryMovie
    {
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int MovieId { get; set; }
        public Movie Movie { get; set; }
    }
}

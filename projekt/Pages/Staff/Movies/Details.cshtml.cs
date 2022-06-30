using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using projekt.Data;
using projekt.Models;

namespace projekt.Pages.Staff.Movies
{
    [Authorize(Policy = "Client")]
    public class DetailsModel : PageModel
    {
        private readonly projekt.Data.ProjectContext _context;

        public DetailsModel(projekt.Data.ProjectContext context)
        {
            _context = context;
        }

        public Movie Movie { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Movie = await _context.Movie.FirstOrDefaultAsync(m => m.Id == id);
            Movie = await _context.Movie.Include(m => m.CategoryMovies).ThenInclude(s => s.Category).AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);

            if (Movie == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}

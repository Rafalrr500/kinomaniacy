using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using projekt.Data;
using projekt.Models;

namespace projekt.Pages.Staff.MovieShowings
{
    [Authorize(Policy = "AdminOrStaff")]
    public class EditModel : PageModel
    {
        private readonly projekt.Data.ProjectContext _context;

        public EditModel(projekt.Data.ProjectContext context)
        {
            _context = context;
        }

        [BindProperty]
        public MovieShow MovieShow { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            MovieShow = await _context.MovieShowing
                .Include(m => m.Movie).FirstOrDefaultAsync(m => m.Id == id);

            if (MovieShow == null)
            {
                return NotFound();
            }
           ViewData["MovieId"] = new SelectList(_context.Movie, "Id", "Name");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(MovieShow).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieShowExists(MovieShow.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool MovieShowExists(int id)
        {
            return _context.MovieShowing.Any(e => e.Id == id);
        }
    }
}

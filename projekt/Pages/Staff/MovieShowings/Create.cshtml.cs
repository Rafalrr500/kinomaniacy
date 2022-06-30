using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using projekt.Data;
using projekt.Models;

namespace projekt.Pages.Staff.MovieShowings
{
    [Authorize(Policy = "AdminOrStaff")]
    public class CreateModel : PageModel
    {
        private readonly projekt.Data.ProjectContext _context;

        public CreateModel(projekt.Data.ProjectContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["MovieId"] = new SelectList(_context.Movie, "Id", "Name");
            return Page();
        }

        [BindProperty]
        public MovieShow MovieShow { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.MovieShowing.Add(MovieShow);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}

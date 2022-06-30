using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using projekt.Data;
using projekt.Models;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;

namespace projekt.Pages.Staff.Movies
{
    [Authorize(Policy = "AdminOrStaff")]
    public class CreateModel : PageModel
    {
        [BindProperty]
        public IList<SelectListItem> CategoryList { get; set; }
        [BindProperty]
        public Movie Movie { get; set; }

        private readonly projekt.Data.ProjectContext _context;

        public CreateModel(projekt.Data.ProjectContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            CategoryList = _context.MovieShow.ToList<Category>().Select(m => new SelectListItem { Text = m.Name, Value = m.Id.ToString() }).ToList<SelectListItem>();
            return Page();
        }


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            IList<CategoryMovie> CategoryMovies = new List<CategoryMovie>();

            foreach (SelectListItem category in CategoryList)
            {
                if (category.Selected)
                {
                    CategoryMovies.Add(new CategoryMovie { CategoryId = Convert.ToInt32(category.Value) });
                }
            }
            Movie.CategoryMovies = CategoryMovies;

            _context.Movie.Add(Movie);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}

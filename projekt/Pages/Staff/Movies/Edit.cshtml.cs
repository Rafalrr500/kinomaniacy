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

namespace projekt.Pages.Staff.Movies
{
    [Authorize(Policy = "AdminOrStaff")]
    public class EditModel : PageModel
    {
        [BindProperty]
        public IList<SelectListItem> CategoryList { get; set; }
        [BindProperty]
        public Movie Movie { get; set; }

        private readonly projekt.Data.ProjectContext _context;

        public EditModel(projekt.Data.ProjectContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Movie = await _context.Movie.Include(m => m.CategoryMovies).FirstOrDefaultAsync(m => m.Id == id);

            CategoryList = _context.MovieShow.ToList<Category>().Select(m => new SelectListItem
            {
                Text = m.Name,
                Value = m.Id.ToString(),
                Selected = Movie.CategoryMovies.Any(S => S.CategoryId == m.Id) ? true : false
            }).ToList<SelectListItem>();

            if (Movie == null)
            {
                return NotFound();
            }
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


            Movie MovieFromDB = await _context.Movie.Include(m => m.CategoryMovies).FirstOrDefaultAsync(m => m.Id == Movie.Id);
            IList<CategoryMovie> CategoryMovies = new List<CategoryMovie>();
            IList<CategoryMovie> CategoriesToRemove = new List<CategoryMovie>();
            IList<CategoryMovie> CategoriesToAdd = new List<CategoryMovie>();
            foreach (SelectListItem category in CategoryList)
            {
                if (category.Selected)
                {
                    CategoryMovies.Add(new CategoryMovie
                    { MovieId = Movie.Id, CategoryId = Convert.ToInt32(category.Value) });

                    CategoryMovie selectedMovie = MovieFromDB.CategoryMovies.Where(m => m.CategoryId == Convert.ToInt32(category.Value)).FirstOrDefault();
                    if (selectedMovie == null)
                    {
                        CategoriesToAdd.Add(new CategoryMovie
                        { MovieId = Movie.Id, CategoryId = Convert.ToInt32(category.Value) });

                    }
                }
            }

            foreach (CategoryMovie categoryMovie in MovieFromDB.CategoryMovies)
            {
                if (CategoryMovies.Any(e => e.MovieId == categoryMovie.MovieId && e.CategoryId == categoryMovie.CategoryId) == false)
                {
                    CategoriesToRemove.Add(categoryMovie);
                }

            }

            MovieFromDB.Name = Movie.Name;
            MovieFromDB.RunningTime = Movie.RunningTime;

            _context.RemoveRange(CategoriesToRemove);

            foreach (var cc in CategoriesToAdd)
            {

                MovieFromDB.CategoryMovies.Add(cc);
            }

            //_context.Attach(Movie).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieExists(Movie.Id))
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

        private bool MovieExists(int id)
        {
            return _context.Movie.Any(e => e.Id == id);
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using projekt.Data;
using projekt.Models;

namespace projekt.Pages.Staff.Categories
{
    [Authorize(Policy = "Client")]
    public class DetailsModel : PageModel
    {
        private readonly projekt.Data.ProjectContext _context;

        public DetailsModel(projekt.Data.ProjectContext context)
        {
            _context = context;
        }

        public Category Category { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Category = await _context.MovieShow.Include(m => m.CategoryMovies).ThenInclude(s => s.Movie).AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);

            if (Category == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}

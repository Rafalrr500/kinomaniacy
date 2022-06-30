using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using projekt.Data;
using projekt.Models;

namespace projekt.Pages.Staff.MovieShowings
{
    public class IndexModel : PageModel
    {
        private readonly projekt.Data.ProjectContext _context;

        public IndexModel(projekt.Data.ProjectContext context)
        {
            _context = context;
        }

        public IList<MovieShow> MovieShow { get;set; }

        public async Task OnGetAsync()
        {
            MovieShow = await _context.MovieShowing
                .Include(m => m.Movie).ToListAsync();
        }
    }
}

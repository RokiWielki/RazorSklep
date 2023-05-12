using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorSklep.Data;
using RazorSklep.Models;

namespace RazorSklep.Pages.Movies
{
    public class IndexModel : PageModel
    {
        private readonly RazorSklep.Data.RazorSklepContext _context;

        public IndexModel(RazorSklep.Data.RazorSklepContext context)
        {
            _context = context;
        }

        public IList<Movie> Movie { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Movie != null)
            {
                Movie = await _context.Movie.ToListAsync();
            }
        }
    }
}

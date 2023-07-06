using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorSklep.Data;
using RazorSklep.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace RazorSklep.Pages.Movies
{
    public class KoszykModel : PageModel
    {
        private readonly RazorSklep.Data.RazorSklepContext _context;

        public KoszykModel(RazorSklep.Data.RazorSklepContext context)
        {
            _context = context;
        }

        public IList<Movie> Movie { get; set; } = default!;
        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }
        public SelectList? Genres { get; set; }
        [BindProperty(SupportsGet = true)]
        public string? MovieGenre { get; set; }

        public async Task OnGetAsync()
        {
            // Use LINQ to get list of genres.
            IQueryable<string> genreQuery = from m in _context.Movie
                                            orderby m.Genre
                                            select m.Genre;

            var movies = from m in _context.Movie
                         select m;

            if (!string.IsNullOrEmpty(SearchString))
            {
                movies = movies.Where(s => s.Title.Contains(SearchString));
            }

            if (!string.IsNullOrEmpty(MovieGenre))
            {
                movies = movies.Where(x => x.Genre == MovieGenre);
            }
            Genres = new SelectList(await genreQuery.Distinct().ToListAsync());
            Movie = await movies.ToListAsync();
        }
        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Movie == null)
            {
                return NotFound();
            }
            var movie = await _context.Movie.FindAsync(id);

            if (movie != null)
            {
                Movie = (IList<Movie>)movie;
                _context.Movie.Remove((Movie)Movie);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Koszyk");
        }
    }
}

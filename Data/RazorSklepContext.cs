using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RazorSklep.Models;

namespace RazorSklep.Data
{
    public class RazorSklepContext : DbContext
    {
        public RazorSklepContext (DbContextOptions<RazorSklepContext> options)
            : base(options)
        {
        }

        public DbSet<RazorSklep.Models.Movie> Movie { get; set; } = default!;
    }
}

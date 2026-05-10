using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SeedLibrary.Data;
using SeedLibrary.Models;

namespace SeedLibrary.Pages.PlantingDates
{
    public class IndexModel : PageModel
    {
        private readonly SeedLibrary.Data.SeedContext _context;

        public IndexModel(SeedLibrary.Data.SeedContext context)
        {
            _context = context;
        }

        public IList<PlantingDate> PlantingDate { get;set; } = default!;

        public async Task OnGetAsync()
        {
            PlantingDate = await _context.PlantingDates.ToListAsync();
        }
    }
}

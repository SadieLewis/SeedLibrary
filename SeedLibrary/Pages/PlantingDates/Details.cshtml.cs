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
    public class DetailsModel : PageModel
    {
        private readonly SeedLibrary.Data.SeedContext _context;

        public DetailsModel(SeedLibrary.Data.SeedContext context)
        {
            _context = context;
        }

        public PlantingDate PlantingDate { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plantingdate = await _context.PlantingDates.FirstOrDefaultAsync(m => m.Id == id);

            if (plantingdate is not null)
            {
                PlantingDate = plantingdate;

                return Page();
            }

            return NotFound();
        }
    }
}

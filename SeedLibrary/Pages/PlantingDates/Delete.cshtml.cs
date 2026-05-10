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
    public class DeleteModel : PageModel
    {
        private readonly SeedLibrary.Data.SeedContext _context;

        public DeleteModel(SeedLibrary.Data.SeedContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plantingdate = await _context.PlantingDates.FindAsync(id);
            if (plantingdate != null)
            {
                PlantingDate = plantingdate;
                _context.PlantingDates.Remove(PlantingDate);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}

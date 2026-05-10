using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SeedLibrary.Data;
using SeedLibrary.Models;

namespace SeedLibrary.Pages.PlantingDates
{
    public class EditModel : PageModel
    {
        private readonly SeedLibrary.Data.SeedContext _context;

        public EditModel(SeedLibrary.Data.SeedContext context)
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

            var plantingdate =  await _context.PlantingDates.FirstOrDefaultAsync(m => m.Id == id);
            if (plantingdate == null)
            {
                return NotFound();
            }
            PlantingDate = plantingdate;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(PlantingDate).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlantingDateExists(PlantingDate.Id))
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

        private bool PlantingDateExists(int id)
        {
            return _context.PlantingDates.Any(e => e.Id == id);
        }
    }
}

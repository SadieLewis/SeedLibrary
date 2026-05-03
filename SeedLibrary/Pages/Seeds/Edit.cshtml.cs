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

namespace SeedLibrary.Pages_Seeds
{
    public class EditModel : PageModel
    {
        private readonly SeedLibrary.Data.SeedContext _context;

        public EditModel(SeedLibrary.Data.SeedContext context)
        {
            _context = context;
        }

        [BindProperty]
        public SeedPacket SeedPacket { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var seedpacket =  await _context.SeedPackets.FirstOrDefaultAsync(m => m.SeedId == id);
            if (seedpacket == null)
            {
                return NotFound();
            }
            SeedPacket = seedpacket;
           ViewData["VarietyName"] = new SelectList(_context.Varieties, "VarietyName", "VarietyName");
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

            _context.Attach(SeedPacket).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SeedPacketExists(SeedPacket.SeedId))
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

        private bool SeedPacketExists(int id)
        {
            return _context.SeedPackets.Any(e => e.SeedId == id);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SeedLibrary.Data;
using SeedLibrary.Models;

namespace SeedLibrary.Pages.Seeds
{
    public class DeleteModel : PageModel
    {
        private readonly SeedLibrary.Data.SeedContext _context;

        public DeleteModel(SeedLibrary.Data.SeedContext context)
        {
            _context = context;
        }

        [BindProperty]
        public SeedPacket Seed { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Seed = await _context.SeedPackets.FirstOrDefaultAsync(m => m.SeedId == id);

            if (Seed is not null)
            {
                Seed = Seed;

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

            Seed = await _context.SeedPackets.FirstOrDefaultAsync(m => m.SeedId == id);

            if (Seed == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
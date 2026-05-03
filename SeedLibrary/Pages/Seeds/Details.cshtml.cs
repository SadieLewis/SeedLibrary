using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SeedLibrary.Data;
using SeedLibrary.Models;

namespace SeedLibrary.Pages_Seeds
{
    public class DetailsModel : PageModel
    {
        private readonly SeedLibrary.Data.SeedContext _context;

        public DetailsModel(SeedLibrary.Data.SeedContext context)
        {
            _context = context;
        }

        public SeedPacket SeedPacket { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var seedpacket = await _context.SeedPackets.FirstOrDefaultAsync(m => m.SeedId == id);

            if (seedpacket is not null)
            {
                SeedPacket = seedpacket;

                return Page();
            }

            return NotFound();
        }
    }
}

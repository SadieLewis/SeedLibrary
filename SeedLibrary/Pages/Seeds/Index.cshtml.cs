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
    public class IndexModel : PageModel
    {
        private readonly SeedLibrary.Data.SeedContext _context;

        public IndexModel(SeedLibrary.Data.SeedContext context)
        {
            _context = context;
        }

        public IList<SeedPacket> SeedPacket { get;set; } = default!;

        public async Task OnGetAsync()
        {
            SeedPacket = await _context.SeedPackets
                .Include(s => s.Variety)
                    .ThenInclude(v => v.CommonName)
                .Include(s => s.Donations)
                    .ThenInclude(d => d.Source)
                .Include(s => s.Growings)
                    .ThenInclude(g => g.PlantingDate)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}

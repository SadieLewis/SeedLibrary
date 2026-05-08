using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SeedLibrary.Data;
using SeedLibrary.Models;

namespace SeedLibrary.Pages.Donations
{
    public class IndexModel : PageModel
    {
        private readonly SeedContext _context;
        private readonly IConfiguration Configuration;

        public IList<Donation> Donation { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Donation = await _context.Donations.Include(d => d.Source)
            .Include(s=>s.SeedPacket)
            .ThenInclude(v=>v.Variety)
            .ToListAsync();
        }


        public IndexModel(SeedContext context, IConfiguration configuration)
        {
            _context = context;
            Configuration = configuration;
        }


        }
    }
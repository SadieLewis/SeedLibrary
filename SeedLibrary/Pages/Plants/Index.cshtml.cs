using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SeedLibrary.Data;
using SeedLibrary.Models;

namespace SeedLibrary.Pages.Plants
{
    public class IndexModel : PageModel
    {
        private readonly SeedContext _context;
        private readonly IConfiguration Configuration;

        public IList<CommonName> CommonName { get;set; } = default!;

        public async Task OnGetAsync()
        {
            CommonName = await _context.CommonNames.Include(v => v.Varieties)
            .ToListAsync();
        }


        public IndexModel(SeedContext context, IConfiguration configuration)
        {
            _context = context;
            Configuration = configuration;
        }


        }
    }
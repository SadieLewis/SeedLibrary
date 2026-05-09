using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SeedLibrary.Data;
using SeedLibrary.Models;

namespace SeedLibrary.Pages.Varieties
{
    public class IndexModel : PageModel
    {
        private readonly SeedContext _context;
        private readonly IConfiguration Configuration;

        public IList<Variety> Variety { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Variety = await _context.Varieties.ToListAsync();
        }


        public IndexModel(SeedContext context, IConfiguration configuration)
        {
            _context = context;
            Configuration = configuration;
        }


        }
    }
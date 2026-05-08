using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SeedLibrary.Data;
using SeedLibrary.Models;

namespace SeedLibrary.Pages.Sources
{
    public class IndexModel : PageModel
    {
        private readonly SeedContext _context;
        private readonly IConfiguration Configuration;

        public IList<Source> Source { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Source = await _context.Sources.ToListAsync();
        }


        public IndexModel(SeedContext context, IConfiguration configuration)
        {
            _context = context;
            Configuration = configuration;
        }


        }
    }
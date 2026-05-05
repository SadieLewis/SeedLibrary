using SeedLibrary.Models.SeedViewModels;
using SeedLibrary.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SeedLibrary.Models;
using Microsoft.AspNetCore.Mvc;

namespace SeedLibrary.Pages.Order
{
    public class OrderModel : PageModel
    {
        private readonly SeedLibrary.Data.SeedContext _context;
        private readonly IConfiguration Configuration;

        public OrderModel(SeedContext context, IConfiguration configuration)
        {
            _context = context;
            Configuration = configuration;
        }
        public string VarietySort { get; set; }
        public string CountSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }
        public PaginatedList<SeedPacket> Seeds { get;set; } = default!;
        public List<int> SelectedSeeds { get; set; } = new();

        public async Task OnGetAsync(string sortOrder,
            string currentFilter, string searchString, int? pageIndex)
        {
            CurrentSort = sortOrder;
            CountSort = string.IsNullOrEmpty(sortOrder) ? "count_desc" : "";
            VarietySort = sortOrder == "Variety" ? "variety_desc" : "Variety";
            if (searchString != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            CurrentFilter = searchString;

            IQueryable<SeedPacket> seedsIQ = _context.SeedPackets
                .Include(s => s.Variety)
                    .ThenInclude(v => v.CommonName)
                .Include(s => s.Donations)
                    .ThenInclude(d => d.Source)
                .Include(s => s.Growings)
                    .ThenInclude(g => g.PlantingDate);

            if (!string.IsNullOrEmpty(searchString))
            {
                seedsIQ = seedsIQ.Where(s =>
                    s.Note.Contains(searchString) ||
                    s.Variety.VarietyName.Contains(searchString) ||
                    s.Variety.CommonName.Name.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "count_desc":
                    seedsIQ = seedsIQ.OrderByDescending(s => s.Count);
                    break;
                case "Variety":
                    seedsIQ = seedsIQ.OrderBy(s => s.Variety.VarietyName);
                    break;
                case "variety_desc":
                    seedsIQ = seedsIQ.OrderByDescending(s => s.Variety.VarietyName);
                    break;
                default:
                    seedsIQ = seedsIQ.OrderBy(s => s.Count);
                    break;
            }

            var pageSize = Configuration.GetValue("PageSize", 5);
            Seeds = await PaginatedList<SeedPacket>.CreateAsync(seedsIQ.AsNoTracking(), pageIndex ?? 1, pageSize);
        }
        public IActionResult OnPost()
        {
            return Page();
        }
        
    }
}
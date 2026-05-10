using SeedLibrary.Models.SeedViewModels;
using SeedLibrary.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SeedLibrary.Models;

using Microsoft.AspNetCore.Mvc;
using System.Dynamic;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Scripting.Hosting;

namespace SeedLibrary.Pages.SpotlightSeeds;

public class SpotlightSeedsModel : PageModel
{
    private readonly SeedLibrary.Data.SeedContext _context;
    private readonly IConfiguration Configuration;
        public SpotlightSeedsModel(SeedContext context, IConfiguration configuration)
        {
            _context = context;
            Configuration = configuration;
        }
        public string CommonNameSort { get; set; }
        public string VarietySort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }
        public PaginatedList<SeedPacket> Seeds { get;set; } = default!;
        public List<SeedPacket> InSeasonSeeds { get; set; } = new();

        public async Task OnGetAsync(string sortOrder,
            string currentFilter, string searchString, int? pageIndex)
        {
            CurrentSort = sortOrder;
            CommonNameSort = string.IsNullOrEmpty(sortOrder) ? "commonname_desc" : "";
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
                    s.Variety.VarietyName.Contains(searchString) ||
                    s.Variety.CommonName.Name.Contains(searchString) ||
                    s.Note.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "commonname_desc":
                    seedsIQ = seedsIQ.OrderByDescending(s => s.Variety.CommonName.Name);
                    break;
                case "Variety":
                    seedsIQ = seedsIQ.OrderBy(s => s.Variety.VarietyName);
                    break;
                case "variety_desc":
                    seedsIQ = seedsIQ.OrderByDescending(s => s.Variety.VarietyName);
                    break;
                default:
                    seedsIQ = seedsIQ.OrderBy(s => s.Variety.CommonName.Name);
                    break;
            }


            var pageSize = Configuration.GetValue("PageSize", 5);
            Seeds = await PaginatedList<SeedPacket>.CreateAsync(seedsIQ.AsNoTracking(), pageIndex ?? 1, pageSize);
            int currentMonth = DateTime.Now.Month;

            InSeasonSeeds = await _context.SeedPackets
                .Include(s => s.Variety)
                    .ThenInclude(v => v.CommonName)
                .Include(s => s.Growings)
                    .ThenInclude(g => g.PlantingDate)
                .Where(s => s.Growings.Any(g =>
                    g.PlantingDate.StartMonth <= currentMonth &&
                    g.PlantingDate.EndMonth >= currentMonth))
                .AsNoTracking()
                .ToListAsync();
        }
}
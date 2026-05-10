using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SeedLibrary.Data;
using SeedLibrary.Models;

namespace SeedLibrary.Pages_Seeds
{
    public class IndexModel : PageModel
    {
        private readonly SeedContext _context;
        private readonly IConfiguration Configuration;

        public IndexModel(SeedContext context, IConfiguration configuration)
        {
            _context = context;
            Configuration = configuration;
        }

        public string VarietySort { get; set; }
        public string CommonNameSort { get; set; }
        public string YearSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }

        public PaginatedList<SeedPacket> SeedPacket { get; set; } = default!;

        public async Task OnGetAsync(string sortOrder, string currentFilter, string searchString, int? pageIndex)
        {
            CurrentSort = sortOrder;
            VarietySort = sortOrder == "Variety" ? "variety_desc" : "Variety";
            CommonNameSort = sortOrder == "CommonName" ? "commonname_desc" : "CommonName";
            YearSort = sortOrder == "Year" ? "year_desc" : "Year";

            if (searchString != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            CurrentFilter = searchString;

            IQueryable<SeedPacket> seedIQ = _context.SeedPackets
                .Include(s => s.Variety)
                    .ThenInclude(v => v.CommonName)
                .Include(s => s.Donations)
                    .ThenInclude(d => d.Source)
                .Include(s => s.Growings)
                    .ThenInclude(g => g.PlantingDate);

            if (!string.IsNullOrEmpty(searchString))
            {
                seedIQ = seedIQ.Where(s =>
                    s.Note.Contains(searchString) ||
                    s.Variety.VarietyName.Contains(searchString) ||
                    s.Variety.CommonName.Name.Contains(searchString) ||
                    s.Donations.Any(d => d.Source.SourceName.Contains(searchString)) ||
                    s.Donations.Any(d => d.Year.ToString().Contains(searchString)));
            }

            switch (sortOrder)
            {
                case "count_desc":
                    seedIQ = seedIQ.OrderByDescending(s => s.Count);
                    break;
                case "Variety":
                    seedIQ = seedIQ.OrderBy(s => s.Variety.VarietyName);
                    break;
                case "variety_desc":
                    seedIQ = seedIQ.OrderByDescending(s => s.Variety.VarietyName);
                    break;
                case "CommonName":
                    seedIQ = seedIQ.OrderBy(s => s.Variety.CommonName.Name);
                    break;
                case "commonname_desc":
                    seedIQ = seedIQ.OrderByDescending(s => s.Variety.CommonName.Name);
                    break;
                case "Year":
                    seedIQ = seedIQ.OrderBy(s => s.Donations.Min(d => d.Year));
                    break;
                case "year_desc":
                    seedIQ = seedIQ.OrderByDescending(s => s.Donations.Max(d => d.Year));
                    break;
                default:
                    seedIQ = seedIQ.OrderBy(s => s.Variety.CommonName.Name);
                    break;
            }

            var pageSize = Configuration.GetValue("PageSize", 10);

            SeedPacket = await PaginatedList<SeedPacket>.CreateAsync(
                seedIQ.AsNoTracking(),
                pageIndex ?? 1,
                pageSize);
        }
    }
}
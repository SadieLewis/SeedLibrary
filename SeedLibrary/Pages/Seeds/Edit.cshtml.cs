using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SeedLibrary.Data;
using SeedLibrary.Models;

namespace SeedLibrary.Pages.Seeds
{
    public class EditModel : PageModel
    {
        private readonly SeedContext _context;

        public EditModel(SeedContext context)
        {
            _context = context;
        }

        [BindProperty]
        public SeedPacket SeedPacket { get; set; } = default!;

        [BindProperty]
        public List<int> SelectedPlantingDateIds { get; set; } = new List<int>();

        public List<SelectListItem> PlantingDateOptions { get; set; } = new();
        public Donation? ExistingDonation { get; set; }
        public string CurrentCommonName { get; set; } = string.Empty;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null) return NotFound();

            SeedPacket = await _context.SeedPackets
                .Include(s => s.Variety)
                .Include(s => s.Donations)
                .Include(s => s.Growings)
                .FirstOrDefaultAsync(m => m.SeedId == id);

            if (SeedPacket == null) return NotFound();

            // Pre-select existing planting dates
            SelectedPlantingDateIds = SeedPacket.Growings
                .Select(g => g.PlantingDatesId)
                .ToList();

            // Get existing donation
            ExistingDonation = SeedPacket.Donations.FirstOrDefault();

            // Set current common name for the script
            CurrentCommonName = SeedPacket.Variety?.CommonNameName ?? string.Empty;

            PopulateDropdowns();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                PopulateDropdowns();
                return Page();
            }

            // 1. Update SeedPacket
            _context.Attach(SeedPacket).State = EntityState.Modified;

            // 2. Update Donation - remove old, add new
            var existingDonations = _context.Donations.Where(d => d.SeedId == SeedPacket.SeedId);
            _context.Donations.RemoveRange(existingDonations);
            var donation = new Donation
            {
                SeedId = SeedPacket.SeedId,
                SourceId = int.Parse(Request.Form["Donation.SourceId"]),
                Year = int.Parse(Request.Form["Donation.Year"])
            };
            _context.Donations.Add(donation);

            // 3. Update Growings - remove old, add new
            var existingGrowings = _context.Growings.Where(g => g.SeedId == SeedPacket.SeedId);
            _context.Growings.RemoveRange(existingGrowings);
            foreach (var plantingDateId in SelectedPlantingDateIds)
            {
                _context.Growings.Add(new Growing
                {
                    SeedId = SeedPacket.SeedId,
                    PlantingDatesId = plantingDateId
                });
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SeedPacketExists(SeedPacket.SeedId))
                    return NotFound();
                else
                    throw;
            }

            return RedirectToPage("./Index");
        }

        private bool SeedPacketExists(int id)
        {
            return _context.SeedPackets.Any(e => e.SeedId == id);
        }

        private void PopulateDropdowns()
        {
            ViewData["CommonName"] = new SelectList(_context.CommonNames, "Name", "Name");
            ViewData["SourceId"] = new SelectList(_context.Sources, "Id", "SourceName");
            ViewData["VarietiesJson"] = System.Text.Json.JsonSerializer.Serialize(
                _context.Varieties.Select(v => new { v.VarietyName, v.CommonNameName }).ToList()
            );

            PlantingDateOptions = _context.PlantingDates.Select(p => new SelectListItem
            {
                Value = p.Id.ToString(),
                Text = $"{p.StartMonth} - {System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(p.StartMonth)}" +
                       $" to {p.EndMonth} - {System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(p.EndMonth)}"
            }).ToList();
        }
    }
}
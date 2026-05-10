using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SeedLibrary.Data;
using SeedLibrary.Models;

namespace SeedLibrary.Pages.Seeds
{
    public class CreateModel : PageModel
    {
        private readonly SeedContext _context;

        public CreateModel(SeedContext context)
        {
            _context = context;
        }

        [BindProperty]
        public SeedPacket SeedPacket { get; set; } = default!;

        [BindProperty]
        public Donation Donation { get; set; } = default!;

        [BindProperty]
        public List<int> SelectedPlantingDateIds { get; set; } = new List<int>();
        public List<SelectListItem> PlantingDateOptions { get; set; } = new();

        public IActionResult OnGet()
        {
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

            _context.SeedPackets.Add(SeedPacket);
            await _context.SaveChangesAsync();

            Donation.SeedId = SeedPacket.SeedId;
            _context.Donations.Add(Donation);

            foreach (var plantingDateId in SelectedPlantingDateIds)
            {
                var plantingDate = await _context.PlantingDates.FindAsync(plantingDateId);
                if (plantingDate != null)
                {
                    _context.Growings.Add(new Growing
                    {
                        SeedId = SeedPacket.SeedId,
                        PlantingDatesId = plantingDateId,
                        PlantingDate = plantingDate
                    });
                }
            }

            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

        private void PopulateDropdowns()
        {
            ViewData["CommonName"] = new SelectList(_context.CommonNames, "Name", "Name");
            ViewData["VarietiesJson"] = System.Text.Json.JsonSerializer.Serialize(
                _context.Varieties.Select(v => new { v.VarietyName, v.CommonNameName }).ToList()
            );
            ViewData["SourceId"] = new SelectList(_context.Sources, "Id", "SourceName");

            PlantingDateOptions = _context.PlantingDates.Select(p => new SelectListItem
            {
                Value = p.Id.ToString(),
                Text = $"{p.StartMonth} - {System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(p.StartMonth)}" +
                    $" to {p.EndMonth} - {System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(p.EndMonth)}"
            }).ToList();
        }
    }
}
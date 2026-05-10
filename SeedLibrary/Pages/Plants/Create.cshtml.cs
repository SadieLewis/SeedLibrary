using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SeedLibrary.Data;
using SeedLibrary.Models;

namespace SeedLibrary.Pages.Plants
{
    public class CreateModel : PageModel
    {
        private readonly SeedLibrary.Data.SeedContext _context;

        public CreateModel(SeedLibrary.Data.SeedContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public CommonName CommonName { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.CommonNames.Add(CommonName);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
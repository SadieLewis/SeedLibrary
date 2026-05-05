using System.ComponentModel.DataAnnotations;

namespace SeedLibrary.Models
{
    public class Source
    {
        public int Id { get; set; }
        [Display(Name = "Source Name")]
        public string SourceName { get; set; } = null!;

        public ICollection<Donation> Donations { get; set; } = new List<Donation>();
    }
}
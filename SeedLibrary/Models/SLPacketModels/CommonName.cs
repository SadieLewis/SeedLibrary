using System.ComponentModel.DataAnnotations;

namespace SeedLibrary.Models
{
    public class CommonName
    {
        [Key]
        [Display(Name = "Common Name")]
        public string Name { get; set; } = string.Empty;

        public ICollection<Variety> Varieties { get; set; } = new List<Variety>();
    }
}
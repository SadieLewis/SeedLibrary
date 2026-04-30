using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SeedLibrary.Models
{
    public class SeedPacket
    {
        [Key]
        public int SeedId {get; set;}
        public int Count {get; set;}
        public string Note {get; set;}

        public string VarietyName { get; set; } = null!;

        [ForeignKey(nameof(VarietyName))]
        public Variety Variety { get; set; } = null!;

        public string CommonNameName {get; set;}
        [ForeignKey(nameof(CommonNameName))]
        public CommonName CommonName {get; set;}

        public ICollection<Donation> Donations { get; set; } = new List<Donation>();
        public ICollection<Growing> Growings { get; set; } = new List<Growing>();
    }
}
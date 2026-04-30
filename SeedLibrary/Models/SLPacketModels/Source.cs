namespace SeedLibrary.Models
{
    public class Source
    {
        public int Id { get; set; }

        public string SourceName { get; set; } = null!;

        public ICollection<Donation> Donations { get; set; } = new List<Donation>();
    }
}
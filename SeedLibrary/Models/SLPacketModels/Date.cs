namespace SeedLibrary.Models
{
    public class Date
    {
        public int Id { get; set; }

        public string StartMonth { get; set; }

        public string EndMonth { get; set; }

        public ICollection<Growing> Growings { get; set; } = new List<Growing>();
    }
}
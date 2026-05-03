namespace SeedLibrary.Models
{
    public class PlantingDate
    {
        public int Id { get; set; }

        public int StartMonth { get; set; }

        public int EndMonth { get; set; }

        public ICollection<Growing> Growings { get; set; } = new List<Growing>();

    }
}
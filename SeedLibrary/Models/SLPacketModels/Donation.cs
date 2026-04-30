namespace SeedLibrary.Models
{
    public class Donation
    {
        public int SourceId { get; set; }
        public Source Source { get; set; } = null!;

        public int SeedId { get; set; }
        public SeedPacket SeedPacket { get; set; } = null!;

        public int Year { get; set; }
    }
}
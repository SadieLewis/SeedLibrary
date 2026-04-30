namespace SeedLibrary.Models
{
    public class Growing
    {
        public int DatesId { get; set; }
        public Date Date { get; set; } = null!;

        public int SeedId { get; set; }
        public SeedPacket SeedPacket { get; set; } = null!;
    }
}
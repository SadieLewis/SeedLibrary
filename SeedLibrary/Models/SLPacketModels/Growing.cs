namespace SeedLibrary.Models
{
    public class Growing
    {
        public int PlantingDatesId { get; set; }
        public PlantingDate PlantingDate { get; set; } = null!;

        public int SeedId { get; set; }
        public SeedPacket SeedPacket { get; set; } = null!;
    }
}
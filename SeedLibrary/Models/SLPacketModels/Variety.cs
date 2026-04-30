using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NuGet.Protocol.Plugins;

namespace SeedLibrary.Models
{
    public class Variety{
        [Key]
        public string VarietyName { get; set; }
        public string Depth { get; set;}
        public string CommonNameName {get; set;}
        [ForeignKey(nameof(CommonNameName))]
        public CommonName CommonName {get; set;}

        public ICollection<SeedPacket> SeedPackets {get; set;} = new List<SeedPacket>();
    }
}
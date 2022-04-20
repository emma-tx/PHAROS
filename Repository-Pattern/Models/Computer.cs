using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using DotNetCore6.Data;


namespace DotNetCore6.Models
{
    public partial class Computer
    {
        [DisplayName("ID")]
        public int Id { get; set; }

        [DisplayName("Host Name")]
        public string? Host { get; set; }

        [DisplayName("IP Address")]
        public string? Ip { get; set; }

        [DisplayName("MAC Address")]
        public string? Mac { get; set; }

        public string? Subnet { get; set; }

        [DisplayName("Broadcast Address")]
        public string? Broadcast { get; set; }

        public string? Notes { get; set; }
        [StringLength(200)]

        [DisplayName("Matched IP")]
        public string? MatchedIp { get; set; }

        [DisplayName("Matched MAC")]
        public string? MatchedMac { get; set; }
        public bool? Loaded { get; set; }
        public string? Matches { get; set; }

        [DisplayName("Updated At")]
        [DataType(DataType.Date)]
        public DateTime? UpdatedAt { get; set; }

        [DisplayName("Created At")]
        [DataType(DataType.Date)]
        public DateTime? CreatedAt { get; set; }

        [DisplayName("Last Login")]
        public DateTime? LastLogin { get; set; }
    }
}

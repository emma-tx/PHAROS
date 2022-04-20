using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DotNetCore6.Models
{
    public partial class Lab
    {
        [DisplayName("Lab ID")]
        public int Id { get; set; }

        [DisplayName("Location")]
        public string? Name { get; set; }

        [DisplayName("Updated On")]
        public DateTime? UpdatedAt { get; set; }

        [DisplayName("Created On")]
        public DateTime? CreatedAt { get; set; }

        [DisplayName("Number of Computers")]
        public int? NumberOfPcs { get; set; }
        public bool? Loaded { get; set; }
    }
}

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DotNetCore6.Models
{
    public partial class User
    {
        [DisplayName("User ID")]
        public int Id { get; set; }

        [StringLength(160)]
        [DisplayName("User")]
        public string? UserName { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Last Updated")]
        public DateTime? UpdatedAt { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Created On")]
        public DateTime? CreatedAt { get; set; }

        [DisplayName("User Loaded")]
        public bool? Loaded { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace MigrationsExample.EF.Models
{
    public partial class File
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public string? Filename { get; set; }
        public string? Location { get; set; }
    }
}

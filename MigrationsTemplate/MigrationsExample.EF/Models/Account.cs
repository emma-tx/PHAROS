using System;
using System.Collections.Generic;

namespace MigrationsExample.EF.Models
{
    public partial class Account
    {
        public int Id { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace Client.Models.DB
{
    public partial class staff
    {
        public int StaffId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? Email { get; set; }
        public int Phone { get; set; }
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}

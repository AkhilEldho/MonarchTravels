using System;
using System.Collections.Generic;

namespace Client.Models.DB
{
    public partial class Client
    {
        public Client()
        {
            Bookings = new HashSet<Booking>();
        }

        public int ClientId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? Email { get; set; }
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;

        public virtual ICollection<Booking> Bookings { get; set; }
    }
}

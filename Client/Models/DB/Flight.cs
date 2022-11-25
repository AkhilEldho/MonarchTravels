using System;
using System.Collections.Generic;

namespace Client.Models.DB
{
    public partial class Flight
    {
        public Flight()
        {
            Bookings = new HashSet<Booking>();
        }

        public int FlightId { get; set; }
        public string FlightName { get; set; } = null!;
        public string? FlightDescription { get; set; }
        public decimal FlightCost { get; set; }

        public virtual ICollection<Booking> Bookings { get; set; }

        public override string ToString()
        {
            return FlightName;
        }
    }
}

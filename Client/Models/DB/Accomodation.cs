using System;
using System.Collections.Generic;

namespace Client.Models.DB
{
    public partial class Accomodation
    {
        public Accomodation()
        {
            Bookings = new HashSet<Booking>();
        }

        public int AccomodationId { get; set; }
        public string AccomodationName { get; set; } = null!;
        public string AccomodationType { get; set; } = null!;
        public string AccomodationAddress { get; set; } = null!;
        public decimal AccomodationCost { get; set; }

        public virtual ICollection<Booking> Bookings { get; set; }

        public override string ToString()
        {
            return AccomodationAddress;
        }
    }
}

using System;
using System.Collections.Generic;

namespace Client.Models.DB
{
    public partial class Tour
    {
        public Tour()
        {
            Bookings = new HashSet<Booking>();
        }

        public int TourId { get; set; }
        public string TourDestination { get; set; } = null!;
        public decimal TourCost { get; set; }

        public virtual ICollection<Booking> Bookings { get; set; }

        public override string ToString()
        {
            return TourDestination;
        }
    }
}

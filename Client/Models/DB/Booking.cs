using System;
using System.Collections.Generic;

namespace Client.Models.DB
{
    public partial class Booking
    {
        public int BookingId { get; set; }
        public int FlightId { get; set; }
        public int ClientId { get; set; }
        public int AccomodationId { get; set; }
        public int TourId { get; set; }
        public int DiscountId { get; set; }
        public decimal? TotalCost { get; set; }

        public virtual Accomodation Accomodation { get; set; } = null!;
        public virtual Client Client { get; set; } = null!;
        public virtual Discount Discount { get; set; } = null!;
        public virtual Flight Flight { get; set; } = null!;
        public virtual Tour Tour { get; set; } = null!;
    }
}

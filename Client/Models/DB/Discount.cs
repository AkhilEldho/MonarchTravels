using System;
using System.Collections.Generic;

namespace Client.Models.DB
{
    public partial class Discount
    {
        public Discount()
        {
            Bookings = new HashSet<Booking>();
        }

        public int DiscountId { get; set; }
        public int DiscountAmount { get; set; }

        public virtual ICollection<Booking> Bookings { get; set; }
    }
}

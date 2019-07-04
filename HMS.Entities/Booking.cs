using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Entities
{
    public class Booking
    {
        public int Id { get; set; }
        public DateTime FromDate { get; set; }
        /// <summary>
        /// No of stay nights
        /// </summary>
        public int Duration { get; set; }

        public int AccommodationId { get; set; }
        public Accommodation Accommodation { get; set; }
    }
}

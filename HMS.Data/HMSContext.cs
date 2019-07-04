using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HMS.Entities;

namespace HMS.Data
{
    public class HMSContext : DbContext 
    {
        public HMSContext()
            : base("HMSConnectionString")
        {

        }

        public DbSet<AccommodationType> AccommodationTypes { get; set; }
        public DbSet<AccommodationPackage> AccommodationPackages { get; set; }
        public DbSet<Accommodation> Accommodations { get; set; }
        public DbSet<Booking> Bookings { get; set; }
    }
}

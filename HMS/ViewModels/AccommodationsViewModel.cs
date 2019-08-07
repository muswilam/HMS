using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HMS.Entities;

namespace HMS.ViewModels
{
    public class AccommodationsViewModel
    {
        public int SelectedAccommodationPackageId { get; set; }

        public AccommodationType AccommodationType { get; set; }
        public IEnumerable<AccommodationPackage> AccommodationPackages { get; set; }
        public IEnumerable<Accommodation> Accommodations { get; set; }
    }

    public class AccommodationPackageDetailsViewModel
    {
        public AccommodationPackage AccommodationPackage { get; set; }
    }
}
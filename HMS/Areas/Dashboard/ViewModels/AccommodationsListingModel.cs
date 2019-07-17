using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HMS.Entities;

namespace HMS.Areas.Dashboard.ViewModels
{
    public class AccommodationsListingModel
    {
        public IEnumerable<Accommodation> Accommodations { get; set; }
        public IEnumerable<AccommodationPackage> AccommodationPackages { get; set; }

        public string SearchByName { get; set; }
        public int? SearchByAccommodationPackageId { get; set; }

    }

    public class AccommodationsActionModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public IEnumerable<AccommodationPackage> AccommodationPackages { get; set; }

        public int AccommodationPackageId { get; set; }
        public AccommodationPackage AccommodationPackage { get; set; }
    }
}
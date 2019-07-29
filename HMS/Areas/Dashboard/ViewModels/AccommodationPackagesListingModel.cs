using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HMS.Entities;
using HMS.ViewModels;

namespace HMS.Areas.Dashboard.ViewModels
{
    public class AccommodationPackagesListingModel
    {
        public IEnumerable<AccommodationPackage> AccommodationPackages { get; set; }
        public IEnumerable<AccommodationType> AccommodationTypes { get; set; }

        public string SearchByName { get; set; }
        public int? SearchByAccommodationTypeId { get; set; }

        public Pager Pager { get; set; }
    }

    public class AccommodationPackagesActionModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int NoOfRoom { get; set; }
        public decimal FeePerNight { get; set; }

        public int AccommodationTypeId { get; set; }
        public AccommodationType AccommodationType { get; set; }

        public string PictureIds { get; set; }

        public IEnumerable<AccommodationType> AccommodationTypes { get; set; }
        public IEnumerable<AccommodationPackagePicture> AccommodationPackagePictures { get; set; }
    }
}
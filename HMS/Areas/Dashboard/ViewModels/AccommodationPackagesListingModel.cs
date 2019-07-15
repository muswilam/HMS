using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HMS.Entities;

namespace HMS.Areas.Dashboard.ViewModels
{
    public class AccommodationPackagesListingModel
    {
        public IEnumerable<AccommodationPackage> AccommodationPackages { get; set; }
        public string SearchTerm { get; set; }  
    }
}
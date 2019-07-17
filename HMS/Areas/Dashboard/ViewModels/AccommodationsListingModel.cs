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

        public string SearchByName { get; set; }
    }
}
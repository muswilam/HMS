using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HMS.Entities;

namespace HMS.Areas.Dashboard.ViewModels
{
    public class AccommodationTypesListingModel
    {
        public IEnumerable<AccommodationType> AccommodationTypes { get; set; }
        public string SearchTerm { get; set; }
    }

    public class AccommodationTypesActionModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
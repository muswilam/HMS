﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HMS.Entities;

namespace HMS.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<AccommodationType> AccommodationTypes { get; set; }

        public IEnumerable<AccommodationPackage> AccommodationPackages { get; set; }
    }
}
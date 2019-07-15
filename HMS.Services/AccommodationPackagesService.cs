using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HMS.Entities;
using HMS.Data;

namespace HMS.Services
{
    public class AccommodationPackagesService
    {
        private HMSContext context;
        public AccommodationPackagesService()
        {
            context = new HMSContext();
        }

        //get list of accommdation packages 
        public IEnumerable<AccommodationPackage> GetAllAccommodationPackages()
        {
            return context.AccommodationPackages.ToList();
        }

        //get accommodation packages by search
        public IEnumerable<AccommodationPackage> GetAccommodationPackagesBySearch(string searchTerm)
        {
            var accommodationPackagesDb = context.AccommodationPackages.AsQueryable();

            if(!string.IsNullOrEmpty(searchTerm))
            {
                accommodationPackagesDb = accommodationPackagesDb.Where(ap => ap.Name.ToLower().Contains(searchTerm.ToLower()));
            }

            return accommodationPackagesDb.AsEnumerable();
        }

        //add accommodation package to db 
        public bool AddAccommodationPackage(AccommodationPackage accommodationPackage)
        {
            context.AccommodationPackages.Add(accommodationPackage);

            return context.SaveChanges() > 0;
        }

    }
}

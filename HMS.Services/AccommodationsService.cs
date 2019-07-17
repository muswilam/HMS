using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HMS.Data;
using HMS.Entities;
using System.Data.Entity;

namespace HMS.Services
{
    public class AccommodationsService
    {
        private HMSContext context;

        public AccommodationsService()
        {
            context = new HMSContext();
        }

        //get list of accommodations
        public IEnumerable<Accommodation> GetAllAccommodations()
        {
            return context.Accommodations.Include(a => a.AccommodationPackage).ToList();
        }

        //get list of accommodaation by search 
        public IEnumerable<Accommodation> GetAccommodationsBySearch(string searchTerm)
        {
            var accommodationsDb = context.Accommodations.Include(a => a.AccommodationPackage).AsQueryable();

            if(!string.IsNullOrEmpty(searchTerm))
            {
                accommodationsDb = accommodationsDb.Where(a => a.Name.ToLower().Contains(searchTerm.ToLower()));
            }

            return accommodationsDb.ToList();
        }

        //add accommodation to db 
        public bool SaveAccommodation(Accommodation accommodation)
        {
            context.Accommodations.Add(accommodation);

            return context.SaveChanges() > 0;
        }
        
    }
}

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

        //get list of accommodaation by search for name or by accommodation package
        public IEnumerable<Accommodation> GetAccommodationsBySearchOrPackageId(string searchTerm , int? accommodationPackageId)
        {
            var accommodationsDb = context.Accommodations.Include(a => a.AccommodationPackage).AsQueryable();

            if(!string.IsNullOrEmpty(searchTerm))
            {
                accommodationsDb = accommodationsDb.Where(a => a.Name.ToLower().Contains(searchTerm.ToLower()));
            }
            if(accommodationPackageId.HasValue)
            {
                accommodationsDb = accommodationsDb.Where(a => a.AccommodationPackageId == accommodationPackageId.Value);
            }
            return accommodationsDb.ToList();
        }

        //get accommdation by id
        public Accommodation GetAccommodationById(int id)
        {
            return context.Accommodations.Single(a => a.Id == id);
        }

        //add accommodation to db 
        public bool SaveAccommodation(Accommodation accommodation)
        {
            context.Accommodations.Add(accommodation);

            return context.SaveChanges() > 0;
        }

        //edit accommodation in db 
        public bool UpdateAccommodation(Accommodation accommodation)
        {
            context.Entry(accommodation).State = EntityState.Modified;

            return context.SaveChanges() > 0;
        }

        //delete accommodation from db 
        public bool DeleteAccommodation(Accommodation accommodation)
        {
            context.Entry(accommodation).State = EntityState.Deleted;

            return context.SaveChanges() > 0;
        }
        
    }
}

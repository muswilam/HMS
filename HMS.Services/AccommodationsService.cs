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
            var accommodationsDb = context.Accommodations.Include(a => a.AccommodationPackage).AsQueryable();
        }

        //get list of accommodations
        public IEnumerable<Accommodation> GetAllAccommodations()
        {
            return context.Accommodations.Include(a => a.AccommodationPackage).ToList();
        }

        //get all accommodations by accommodation package id
        public IEnumerable<Accommodation> GetAllAccommodationsByAccommodationPackage(int accommodationId)
        {
            return context.Accommodations.Where(a => a.AccommodationPackageId == accommodationId).Include(a => a.AccommodationPackage).ToList();
        }

        //get list of accommodaation by search for name or by accommodation package
        public IEnumerable<Accommodation> GetAccommodationsBySearchOrPackageId(string searchTerm , int? accommodationPackageId, int page , int pageSize)
        {
            var accommodationsDb = context.Accommodations.Include(a => a.AccommodationPackage).AsQueryable();

            if(!string.IsNullOrEmpty(searchTerm))
            {
                accommodationsDb = accommodationsDb.Where(a => a.Name.ToLower().Contains(searchTerm.ToLower()));
            }
            if(accommodationPackageId.HasValue && accommodationPackageId.Value > 0)
            {
                accommodationsDb = accommodationsDb.Where(a => a.AccommodationPackageId == accommodationPackageId.Value);
            }

            var skip = (page - 1) * pageSize;

            return accommodationsDb.OrderBy(a => a.AccommodationPackageId).Skip(skip).Take(pageSize).ToList();
        }

        //get total number of records
        public int GetAllAccommodationsCount(string searchTerm, int? accommodationPackageId) 
        {
            var accommodationsDb = context.Accommodations.Include(a => a.AccommodationPackage).AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                accommodationsDb = accommodationsDb.Where(a => a.Name.ToLower().Contains(searchTerm.ToLower()));
            }
            if (accommodationPackageId.HasValue && accommodationPackageId.Value > 0)
            {
                accommodationsDb = accommodationsDb.Where(a => a.AccommodationPackageId == accommodationPackageId.Value);
            }
            
            return accommodationsDb.Count();
        }

        //get accommdation by id
        public Accommodation GetAccommodationById(int id)
        {
            return context.Accommodations.Include(a => a.AccommodationPictures.Select(ap => ap.Picture)).Single(a => a.Id == id);
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

        //delete accommodation pictures by accommodation id 
        public bool DeleteAccommodationPictures(int acccommodationId)
        {
            var existing = context.Accommodations.Find(acccommodationId).AccommodationPictures.ToList();

            foreach (var e in existing)
            {
                context.Entry(e).State = EntityState.Deleted;
            }

            return context.SaveChanges() > 0; 
        }

        //delete accommodation from db 
        public bool DeleteAccommodation(Accommodation accommodation)
        {
            if(accommodation.AccommodationPictures != null && accommodation.AccommodationPictures.Count != 0)
            {
                DeleteAccommodationPictures(accommodation.Id);
            }

            context.Entry(accommodation).State = EntityState.Deleted;

            return context.SaveChanges() > 0;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HMS.Entities;
using HMS.Data;
using System.Data.Entity;

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

        //get accommodation packages by accommodation type 
        public IEnumerable<AccommodationPackage> GetAllAccommodationPackagesByAccommodationType(int accommodationTypeId)
        {
            return context.AccommodationPackages.Where(ap => ap.AccommodationTypeId == accommodationTypeId).ToList();
        }

        //get accommodation packages by search name or by accoommodation type 
        public IEnumerable<AccommodationPackage> GetAccommodationPackagesBySearch(string searchTerm , int? accommoddationTypeId , int page , int recordSize )
        {
            var accommodationPackagesDb = context.AccommodationPackages.Include(a => a.AccommodationType).AsQueryable();

            if(!string.IsNullOrEmpty(searchTerm))
            {
                accommodationPackagesDb = accommodationPackagesDb.Where( ap => ap.Name.ToLower().Contains(searchTerm.ToLower()));
            }
            if(accommoddationTypeId.HasValue && accommoddationTypeId > 0)
            {
                accommodationPackagesDb = accommodationPackagesDb.Where(ap => ap.AccommodationTypeId == accommoddationTypeId.Value);
            }

            // skip = (1-1) * 3 = 0
            var skip = (page -1) * recordSize;

            return accommodationPackagesDb.OrderBy(ap => ap.AccommodationTypeId).Skip(skip).Take(recordSize).ToList();
        }

        //get Number of total reecords 
        public int GetAccommodationPackagesCount(string searchTerm, int? accommoddationTypeId)
        {
            var accommodationPackagesDb = context.AccommodationPackages.Include(a => a.AccommodationType).AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                accommodationPackagesDb = accommodationPackagesDb.Where(ap => ap.Name.ToLower().Contains(searchTerm.ToLower()));
            }
            if (accommoddationTypeId.HasValue && accommoddationTypeId > 0)
            {
                accommodationPackagesDb = accommodationPackagesDb.Where(ap => ap.AccommodationTypeId == accommoddationTypeId.Value);
            }

            return accommodationPackagesDb.Count();
        }

        //get accommoodation package by id
        public AccommodationPackage GetAccommodationPackageById(int id)
        {
            return context.AccommodationPackages.Include(ap => ap.AccommodationPackagePictures.Select(aPP => aPP.Picture)).Single(ap => ap.Id == id);
        }

        //add accommodation package to db 
        public bool AddAccommodationPackage(AccommodationPackage accommodationPackage)
        {
            context.AccommodationPackages.Add(accommodationPackage);
            return context.SaveChanges() > 0;
        }

        //edit accommodation package in db
        public bool UpdateAccommodationPackage(AccommodationPackage accomodationPackage)
        {
            context.Entry(accomodationPackage).State = EntityState.Modified;

            return context.SaveChanges() > 0;
        }

        //delete accommodation package pic by accommodation package id
        public void DeleteAccommdationPackagePictures(int accommodationPackageId)
        {
            var existing = context.AccommodationPackages.Find(accommodationPackageId).AccommodationPackagePictures.ToList();

            foreach (var item in existing)
            {
                context.Entry(item).State = EntityState.Deleted;
            }

            context.SaveChanges();
        }

        //delete accommodation package by id from db
        public bool DeleteAccommodationPackage(AccommodationPackage accommodationPackage)
        {
            if(accommodationPackage.AccommodationPackagePictures != null && accommodationPackage.AccommodationPackagePictures.Count != 0)
            {
                DeleteAccommdationPackagePictures(accommodationPackage.Id);
            }
            context.Entry(accommodationPackage).State = System.Data.Entity.EntityState.Deleted;
            return context.SaveChanges() > 0;
        }
    }
}

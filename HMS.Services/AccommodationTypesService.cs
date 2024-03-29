﻿using HMS.Data;
using HMS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Services
{
    public class AccommodationTypesService
    {
        private HMSContext context;
        public AccommodationTypesService()
        {
            context = new HMSContext();
        }

        //get all accommodation types from Db
        public IEnumerable<AccommodationType> GetAllAccommodationTypes()
        {
            return context.AccommodationTypes.ToList();
        }

        //get accommodation type by search
        public IEnumerable<AccommodationType> GetAccommodationTypesBySearch(string searchTerm)
        {
            var accommodationTypesDb = context.AccommodationTypes.AsQueryable();

            if(!string.IsNullOrEmpty(searchTerm))
            {
                //note) when use AsQueryable you've to put your result in variable to perform changes on this variable
                accommodationTypesDb = accommodationTypesDb.Where(a => a.Name.ToLower().Contains(searchTerm.ToLower()));
            }

            return accommodationTypesDb.AsEnumerable();
        }

        //get one accommodation type by id from Db
        public AccommodationType GetAccommodationTypeById(int id)
        {
            return context.AccommodationTypes.Single(a => a.Id == id);
        }

        //save accommodation type to Db
        public bool SaveAccommodationType(AccommodationType formModelAccommodationType)
        {
            context.AccommodationTypes.Add(formModelAccommodationType);
            return context.SaveChanges() > 0;
        }

        //edit accommodation type and save it to db
        public bool UpdateAccommodationType(AccommodationType formModelAccommodationType)
        {
            context.Entry(formModelAccommodationType).State = System.Data.Entity.EntityState.Modified;
            return context.SaveChanges() > 0;
        }

        //delete accommodation type from db by entrry state
        public bool DeleteAccommodationType(AccommodationType formModelAccommodationType)
        {
            context.Entry(formModelAccommodationType).State = System.Data.Entity.EntityState.Deleted;
            return context.SaveChanges() > 0;
        }
    }
}

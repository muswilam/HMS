using HMS.Data;
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
        public IEnumerable<AccommodationType> GetAllAccommodationTypes()
        {
            var context = new HMSContext();

            return context.AccommodationTypes.ToList();
        }

        public bool SaveAccommodationTypes(AccommodationType formModelAccommodationType)
        {
            var context = new HMSContext();


            context.AccommodationTypes.Add(formModelAccommodationType);
            return context.SaveChanges() > 0;
        }
    }
}

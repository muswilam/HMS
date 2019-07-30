using HMS.Data;
using HMS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Services
{
    public class DashboardService
    {
        private HMSContext context;

        public DashboardService()
        {
            context = new HMSContext();
        }

        //save pic tto db
        public bool SavePicture(Picture picture)
        {
            context.Pictures.Add(picture);

            return context.SaveChanges() > 0;
        }

        //get pics by ids
        public IEnumerable<Picture> GetPicturesByIds(List<int> picturesIds)
        {
            return picturesIds.Select(p => context.Pictures.Find(p)).ToList();
        }

        //get pictures by accommodation Package id
        //public List<AccommodationPackagePicture> GetPicturesByAccommodationPackageId(int accommodationPackageId)
        //{
        //    return context.AccommodationPackages.Find(accommodationPackageId).AccommodationPackagePictures.ToList();
        //}
    }
}

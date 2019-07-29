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
        private HMSContext _context;

        public DashboardService()
        {
            _context = new HMSContext();
        }

        //save pic tto db
        public bool SavePicture(Picture picture)
        {
            _context.Pictures.Add(picture);

            return _context.SaveChanges() > 0;
        }

        //get pics by ids
        public IEnumerable<Picture> GetPicturesByIds(List<int> picturesIds)
        {
            return picturesIds.Select(p => _context.Pictures.Find(p)).ToList();
        }
    }
}

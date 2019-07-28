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
        public bool SavePicture(Picture picture)
        {
            _context.Pictures.Add(picture);

            return _context.SaveChanges() > 0;
        }
    }
}

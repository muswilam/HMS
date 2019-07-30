using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Entities
{
    public class AccommodationPackage
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int NoOfRoom { get; set; }
        public decimal FeePerNight { get; set; }

        public int AccommodationTypeId { get; set; }
        public AccommodationType AccommodationType { get; set; }

        public List<AccommodationPackagePicture> AccommodationPackagePictures { get; set; }
    }
}

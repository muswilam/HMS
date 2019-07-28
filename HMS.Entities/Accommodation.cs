using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Entities
{
    public class Accommodation
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }


        public int AccommodationPackageId { get; set; }
        public AccommodationPackage AccommodationPackage { get; set; }

        public List<AccommodationPicture> AccommodationPictures { get; set; }
    }
}

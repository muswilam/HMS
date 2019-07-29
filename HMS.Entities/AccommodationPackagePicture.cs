using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Entities
{
    public class AccommodationPackagePicture
    {
        public int Id { get; set; }

        public int AccommodationPackageId { get; set; }

        public int PictureId { get; set; }
        public virtual Picture Picture { get; set; }
    }
}

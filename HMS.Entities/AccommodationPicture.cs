using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Entities
{
    public class AccommodationPicture
    {
        public int Id { get; set; }

        public int AccommodationId { get; set; }

        public int PictureId { get; set; }
        public Picture Picture { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVillaAgency.EntityLayer.Concrete
{
    public class Favorite
    {
        public int FavoriteId { get; set; }
        public int AppUserId { get; set; }
        public int HouseId { get; set; }

        
        public House House { get; set; }
        public AppUser AppUser { get; set; }
    }
}

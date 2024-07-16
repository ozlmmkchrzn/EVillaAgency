using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVillaAgency.EntityLayer.Concrete
{
    public class Favorite
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int HouseId { get; set; }

        public User User { get; set; }
        public House House { get; set; }
    }
}

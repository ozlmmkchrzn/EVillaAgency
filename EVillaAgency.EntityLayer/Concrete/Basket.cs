using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVillaAgency.EntityLayer.Concrete
{
    public class Basket
    {
        public int BasketId { get; set; }

        public int HouseId { get; set; }
        public House House { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public bool Status { get; set; }

        public DateTime CreatedDate { get; set; }

        public Order Order { get; set; }

    }
}

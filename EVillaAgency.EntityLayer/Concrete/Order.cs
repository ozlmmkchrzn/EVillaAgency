using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVillaAgency.EntityLayer.Concrete
{
    public class Order
    {
        public int OrderID { get; set; }
        public int BasketId { get; set; }
        public DateTime CreatedDate { get; set; }

        public Basket Basket { get; set; }
    }
}

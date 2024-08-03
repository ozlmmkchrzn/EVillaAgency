using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVillaAgency.EntityLayer.Concrete
{
    public class Coupon
    {
        public int CouponId { get; set; }
        public string Code { get; set; }
        public DateTime StartedDate { get; set; }
        public DateTime EndedDate { get; set; }
        public float DiscountRate { get; set; }
        public bool Status { get; set; }

        public ICollection<Basket> Baskets { get; set; }
    }
}

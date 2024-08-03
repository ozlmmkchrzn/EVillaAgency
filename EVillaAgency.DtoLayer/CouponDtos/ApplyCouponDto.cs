using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVillaAgency.DtoLayer.CouponDtos
{
    public class ApplyCouponDto
    {
        public int BasketId { get; set; }
        public string CouponCode { get; set; }
    }
}

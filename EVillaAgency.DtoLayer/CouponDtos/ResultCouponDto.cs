using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVillaAgency.DtoLayer.CouponDtos
{
    public class ResultCouponDto
    {
        public int CouponId { get; set; }
        public string Code { get; set; }
        public DateTime StartedDate { get; set; }
        public DateTime EndedDate { get; set; }
        public float DiscountRate { get; set; }
        public bool Status { get; set; }
    }
}

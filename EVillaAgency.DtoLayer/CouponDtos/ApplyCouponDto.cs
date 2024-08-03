using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVillaAgency.DtoLayer.CouponDtos
{
    public class ApplyCouponDto
    {
        public int CouponId { get; set; }
        public string Code { get; set; }
        public DateTime StartedDate { get; set; }
        public DateTime EndedDate { get; set; }
        public decimal DiscountRate { get; set; }
        public bool Status { get; set; }
        public bool IsValid { get; set; }
    }
}

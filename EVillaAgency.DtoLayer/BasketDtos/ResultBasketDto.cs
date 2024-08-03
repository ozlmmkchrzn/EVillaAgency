using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVillaAgency.DtoLayer.BasketDtos
{
    public class ResultBasketDto
    {
        public int BasketId { get; set; }
        public string HouseTitle { get; set; }
        public string UserName { get; set; }
        public string OwnerName { get; set; }
        public string HouseTypeName { get; set; }
        public decimal HousePrice { get; set; }
        public bool Status { get; set; }
        public int? CouponId { get; set; }
    }
}

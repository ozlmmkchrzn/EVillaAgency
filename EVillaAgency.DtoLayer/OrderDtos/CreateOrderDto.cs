using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVillaAgency.DtoLayer.OrderDtos
{
    public class CreateOrderDto
    {
        public int BasketId { get; set; }
        public bool Status { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}

using EVillaAgency.DtoLayer.OrderDtos;
using EVillaAgency.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVillaAgency.BusinessLayer.Abstract
{
    public interface IOrderService :IGenericService<Order>
    {
        Task CreateOrder(CreateOrderDto dto);
    }
}

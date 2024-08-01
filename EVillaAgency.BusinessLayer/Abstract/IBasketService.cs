using EVillaAgency.DtoLayer.BasketDtos;
using EVillaAgency.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVillaAgency.BusinessLayer.Abstract
{
    public interface IBasketService :IGenericService<Basket>
    {
        Task<List<ResultBasketDto>> GetAllBAskets();
        Task CreateBasket(CreateBasketDto dto);

        Task<ResultBasketDto> GetBasketById(int id);

        Task<ResultBasketDto> GetLastBasketByUserId(int id);
    }
}

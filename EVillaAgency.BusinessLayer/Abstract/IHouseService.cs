using EVillaAgency.DtoLayer.FavoriteDtos;
using EVillaAgency.DtoLayer.HouseDtos;
using EVillaAgency.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVillaAgency.BusinessLayer.Abstract
{
    public interface IHouseService : IGenericService<House>
    {
        Task<List<ResultHousesWithNames>> GetHouseWithNamesAsync();
        Task<ResultHousesWithNames> GetHouseWithNamesByIdAsync(int id);
        Task<List<ResultHousesWithNames>> GetHousesByHouseTypeId(int id);
        Task<List<ResultHousesWithNames>> GetLast6Houses();
        
    }
}

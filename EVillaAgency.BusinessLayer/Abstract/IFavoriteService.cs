using EVillaAgency.DtoLayer.FavoriteDtos;
using EVillaAgency.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVillaAgency.BusinessLayer.Abstract
{
    public interface IFavoriteService : IGenericService<Favorite>
    {
        Task<ResultHouseNameAndUsernameByFavoriteIdDto> GetHouseNameAndUserNameByFavoriteIdAsync(int id);
        Task<List<ResultAllFavoritesWithNamesDto>> GetAllFavoritesWithNamesAsync();
        Task<List<ResultTop3FavoritedHousesDto>> GetTopFavoritedHousesAsync();
        Task<ResultTop3FavoritedHousesDto> GetMostFavoritedHouseAsync();

    }

}

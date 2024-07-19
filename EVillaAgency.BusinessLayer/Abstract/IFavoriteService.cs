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
    }

}

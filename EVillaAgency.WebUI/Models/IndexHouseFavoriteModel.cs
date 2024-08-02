using EVillaAgency.WebUI.Dtos.FavoriteDtos;
using EVillaAgency.WebUI.Dtos.HouseDtos;

namespace EVillaAgency.WebUI.Models
{
    public class IndexHouseFavoriteModel
    {
        public List<ResultHousesWithNamesDto> LastSixHouses { get; set; }
        public List<ResultTop3FavoritedHousesDto> TopFavoritedHouses { get; set; }

    }
}

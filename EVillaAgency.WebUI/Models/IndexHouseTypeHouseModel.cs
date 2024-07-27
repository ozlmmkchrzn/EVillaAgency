
using EVillaAgency.WebUI.Dtos.HouseDtos;
using EVillaAgency.WebUI.Dtos.HouseTypeDtos;

namespace EVillaAgency.WebUI.Models
{
    public class IndexHouseTypeHouseModel
    {
        public ResultHousesWithNamesDto LatestHouseByHouseType { get; set; }
        public List<ResultHouseTypeDto> HouseTypes { get; set; }
    }
}

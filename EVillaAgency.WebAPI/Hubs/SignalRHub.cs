using EVillaAgency.BusinessLayer.Abstract;
using Microsoft.AspNetCore.SignalR;

namespace EVillaAgency.WebAPI.Hubs
{
	public class SignalRHub:Hub
	{
        private readonly IHouseService _houseService;
        private readonly IFavoriteService _favoriteService;
        private readonly IAppUserService _userService;
        private readonly ICityService _cityService;
        private readonly IDistrictService _districtService;

        public SignalRHub(IHouseService houseService, IFavoriteService favoriteService, IAppUserService userService, IDistrictService districtService, ICityService cityService)
        {
            _houseService = houseService;
            _favoriteService = favoriteService;
            _userService = userService;
            _districtService = districtService;
            _cityService = cityService;
        }
        public async Task SendStatistic()
        {
            var value = await _houseService.GetTotalHousesCountAsync(); // Await the async call
            await Clients.All.SendAsync("GetTotalHousesCount", value);


            var value2 = await _userService.GetTotalUserCountAsync(); // Await the async call
            await Clients.All.SendAsync("GetTotalUsersCount", value2);

            var value3 = await _houseService.GetTotalHouseOwnerCount(); // Await the async call
            await Clients.All.SendAsync("GetTotalOwnerCount", value3);

            var value4 = await _favoriteService.GetFavoritedHouseCountAsync(); // Await the async call
            await Clients.All.SendAsync("GetFavoritedHouseCount", value4);

            var value5 = await _districtService.GetDistrictCountAsync(); // Await the async call
            await Clients.All.SendAsync("GetDistrictCount", value5);

            var value6 = await _cityService.GetCityCountAsync(); // Await the async call
            await Clients.All.SendAsync("GetCityCount", value6);
        }

        public async Task SendLatestHouseByHouseType(int id)
        {
            var house = await _houseService.GetLatestHouseByHouseTypeAsync(id);
            await Clients.All.SendAsync("GetLatestHouseByHouseType", house);
        }

        //var value2 = _favoriteService.GetMostFavoritedHouseAsync();
        //await Clients.All.SendAsync("GetMostFavoritedHouse", value2);

    }
}

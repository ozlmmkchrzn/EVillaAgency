using EVillaAgency.BusinessLayer.Abstract;
using Microsoft.AspNetCore.SignalR;

namespace EVillaAgency.WebAPI.Hubs
{
	public class SignalRHub:Hub
	{
        private readonly IHouseService _houseService;
        private readonly IFavoriteService _favoriteService;
        private readonly IUserService _userService;

        public SignalRHub(IHouseService houseService, IFavoriteService favoriteService, IUserService userService)
        {
            _houseService = houseService;
            _favoriteService = favoriteService;
            _userService = userService;
        }
        public async Task SendStatistic()
        {
            var value = await _houseService.GetTotalHousesCountAsync(); // Await the async call
            await Clients.All.SendAsync("GetTotalHousesCount", value);


            var value2 = await _userService.GetTotalUserCount(); // Await the async call
            await Clients.All.SendAsync("GetTotalUsersCount", value2);

            var value3 = await _houseService.GetTotalHouseOwnerCount(); // Await the async call
            await Clients.All.SendAsync("GetTotalOwnerCount", value3);
        }

        //var value2 = _favoriteService.GetMostFavoritedHouseAsync();
        //await Clients.All.SendAsync("GetMostFavoritedHouse", value2);

    }
}

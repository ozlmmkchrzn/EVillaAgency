using EVillaAgency.BusinessLayer.Abstract;
using Microsoft.AspNetCore.SignalR;

namespace EVillaAgency.WebAPI.Hubs
{
	public class SignalRHub:Hub
	{
        private readonly IHouseService _houseService;
        private readonly IFavoriteService _favoriteService;

        public SignalRHub(IHouseService houseService, IFavoriteService favoriteService)
        {
            _houseService = houseService;
            _favoriteService = favoriteService;
        }
        public async Task SendStatistic()
        {
            var value = await _houseService.GetTotalHousesCountAsync(); // Await the async call
            await Clients.All.SendAsync("GetTotalHousesCount", value);
        }

        //var value2 = _favoriteService.GetMostFavoritedHouseAsync();
        //await Clients.All.SendAsync("GetMostFavoritedHouse", value2);

    }
}

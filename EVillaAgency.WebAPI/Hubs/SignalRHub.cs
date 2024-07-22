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
            var value = _houseService.GetListAsync();
            await Clients.All.SendAsync("GetAllHouseCount", value);

            var value2 = _favoriteService.GetMostFavoritedHouseAsync();
            await Clients.All.SendAsync("GetMostFavoritedHouse", value2);

        }

    }
}

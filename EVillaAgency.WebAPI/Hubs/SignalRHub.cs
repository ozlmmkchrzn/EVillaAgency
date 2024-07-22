using EVillaAgency.BusinessLayer.Abstract;
using Microsoft.AspNetCore.SignalR;

namespace EVillaAgency.WebAPI.Hubs
{
	public class SignalRHub:Hub
	{
        private readonly IHouseService _houseService;

        public SignalRHub(IHouseService houseService)
        {
            _houseService = houseService;
        }
        public async Task SendStatistic()
        {
            var value = _houseService.GetListAsync();
            await Clients.All.SendAsync("GetAllHouseCount", value);

        }

    }
}

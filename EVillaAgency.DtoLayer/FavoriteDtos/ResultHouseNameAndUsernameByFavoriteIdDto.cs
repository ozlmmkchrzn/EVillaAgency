using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVillaAgency.DtoLayer.FavoriteDtos
{
    public class ResultHouseNameAndUsernameByFavoriteIdDto
    {
        public int FavoriteId { get; set; }
        public int HouseId { get; set; }
        public string HouseName { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVillaAgency.DtoLayer.FavoriteDtos
{
    public class CreateFavoriteDto
    {
        public int UserId { get; set; }
        public int HouseId { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVillaAgency.WebUI.Dtos.FovariteDtos
{
    public class ResultTop3FavoritedHousesDto
    {
        public int HouseId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Location { get; set; }
        public string HouseTypeName { get; set; }
        public string OwnerName { get; set; }
        public string DictrictName { get; set; }
        public string CityName { get; set; }
        public int Size { get; set; }
        public int Bedrooms { get; set; }
        public bool Pool { get; set; }
        public int Bathrooms { get; set; }
        public bool Garage { get; set; }
        public bool Garden { get; set; }
        public int YearBuilt { get; set; }
        public string HeatingTypeName { get; set; }
        public int FavoriteCount { get; set; }
    }
}

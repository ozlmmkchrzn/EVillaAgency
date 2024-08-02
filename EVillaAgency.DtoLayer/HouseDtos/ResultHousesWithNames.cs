using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVillaAgency.DtoLayer.HouseDtos
{
    public class ResultHousesWithNames
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
        public string ImageUrl { get; set; }
        public int Size { get; set; }
        public int Bedrooms { get; set; }
        public bool Pool { get; set; }
        public int Bathrooms { get; set; }
        public bool Garage { get; set; }
        public bool Garden { get; set; }
        public int YearBuilt { get; set; }
        public string HeatingType { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool Status { get; set; }
    }
}

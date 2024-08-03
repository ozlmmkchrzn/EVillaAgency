﻿namespace EVillaAgency.WebUI.Dtos.HouseDtos
{
    public class UpdateHouseDto
    {
        public int HouseId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Location { get; set; }
        public int HouseTypeId { get; set; }
        public int OwnerId { get; set; }
        public int Size { get; set; }
        public int Bedrooms { get; set; }
        public bool Pool { get; set; }
        public int Bathrooms { get; set; }
        public bool Garage { get; set; }
        public bool Garden { get; set; }
        public int YearBuilt { get; set; }
        public int HeatingTypeId { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        // Sadece DistrictId tutulur
        public int DistrictId { get; set; }
        public bool Status { get; set; }
    }
}

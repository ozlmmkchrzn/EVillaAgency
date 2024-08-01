namespace EVillaAgency.WebUI.Dtos.BasketDtos
{
    public class ResultBasketDto
    {
        public int BasketId { get; set; }
        public string HouseTitle { get; set; }
        public int HouseId { get; set; }
        public string UserName { get; set; }
        public string OwnerName { get; set; }
        public string HouseTypeName { get; set; }
        public decimal HousePrice { get; set; }
    }
}

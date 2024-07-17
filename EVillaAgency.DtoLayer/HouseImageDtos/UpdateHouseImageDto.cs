using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVillaAgency.DtoLayer.HouseImageDtos
{
    public class UpdateHouseImageDto
    {
        public int Id { get; set; }
        public int HouseId { get; set; }
        public int ImageId { get; set; }
    }
}

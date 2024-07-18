using EVillaAgency.DtoLayer.HouseImageDtos;
using EVillaAgency.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVillaAgency.BusinessLayer.Abstract
{
    public interface IHouseImageService : IGenericService<HouseImage>
    {
        Task<List<ResultHouseImageWithNamesDto>> GetImagesWithNames();
        Task<ResultHouseImageWithNamesDto> GetHouseAndImageNamesByHouseImageId(int id);
    }
}

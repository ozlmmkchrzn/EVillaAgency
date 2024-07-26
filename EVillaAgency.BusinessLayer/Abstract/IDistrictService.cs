using EVillaAgency.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVillaAgency.BusinessLayer.Abstract
{
    public interface IDistrictService : IGenericService<District>
    {
        Task<int> GetDistrictCountAsync();
    }
}

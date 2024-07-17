using EVillaAgency.BusinessLayer.Abstract;
using EVillaAgency.DataAccessLayer.Context;
using EVillaAgency.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVillaAgency.BusinessLayer.Concrete
{
    public class HouseTypeManager : GenericRepository<HouseType>, IHouseTypeService
    {
        public HouseTypeManager(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}

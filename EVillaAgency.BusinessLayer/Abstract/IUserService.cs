using EVillaAgency.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVillaAgency.BusinessLayer.Abstract
{
    public interface IUserService : IGenericService<User>
    {
        Task<int> GetTotalUserCount();
    }
}

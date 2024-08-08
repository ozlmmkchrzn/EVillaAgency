using EVillaAgency.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVillaAgency.BusinessLayer.Abstract
{
    public interface IAppUserService : IGenericService<AppUser>
    {
        Task<int> GetTotalUserCountAsync();
        Task<AppUser> ValidateUserAsync(string email, string password);
    }
}

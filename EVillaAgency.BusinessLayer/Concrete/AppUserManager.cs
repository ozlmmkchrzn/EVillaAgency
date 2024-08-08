using EVillaAgency.BusinessLayer.Abstract;
using EVillaAgency.DataAccessLayer.Context;
using EVillaAgency.EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVillaAgency.BusinessLayer.Concrete
{
    public class AppUserManager : GenericRepository<AppUser>, IAppUserService
    {
        private readonly AppDbContext _appDbContext;

        public AppUserManager(AppDbContext dbContext) : base(dbContext)
        {
            _appDbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<int> GetTotalUserCountAsync()
        {
            try
            {
                return await _appDbContext.Users.CountAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting total users count: {ex.Message}", ex);
            }
        }

        public Task<AppUser> ValidateUserAsync(string email, string password)
        {
            throw new NotImplementedException();
        }
    }
}

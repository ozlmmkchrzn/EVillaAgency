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
    public class UserManager : GenericRepository<User>, IUserService
    {
        private readonly AppDbContext _appDbContext;

        public UserManager(AppDbContext dbContext) : base(dbContext)
        {
            _appDbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<int> GetTotalUserCount()
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
    }
}

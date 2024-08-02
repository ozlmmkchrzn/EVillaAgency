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
    public class DistrictManager : GenericRepository<District>, IDistrictService
    {
        private readonly AppDbContext _appDbContext;
        public DistrictManager(AppDbContext dbContext, AppDbContext appDbContext) : base(dbContext)
        {
            _appDbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<int> GetDistrictCountAsync()
        {
            var count = await _appDbContext.Districts
                .CountAsync();
            return count;

        }
    }
}

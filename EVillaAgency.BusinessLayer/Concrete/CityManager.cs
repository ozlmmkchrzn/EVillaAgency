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
    public class CityManager : GenericRepository<City>, ICityService
    {
        private readonly AppDbContext _appDbContext;
        public CityManager(AppDbContext dbContext) : base(dbContext)
        {
            _appDbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<int> GetCityCountAsync()
        {
            var count = await _appDbContext.Cities.CountAsync();
            return count;
        }
    }
}

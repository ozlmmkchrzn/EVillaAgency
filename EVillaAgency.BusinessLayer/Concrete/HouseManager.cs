using EVillaAgency.BusinessLayer.Abstract;
using EVillaAgency.DataAccessLayer.Context;
using EVillaAgency.DtoLayer.HouseDtos;
using EVillaAgency.EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVillaAgency.BusinessLayer.Concrete
{
    public class HouseManager : GenericRepository<House>, IHouseService
    {
        private readonly AppDbContext _appDbContext;
        public HouseManager(AppDbContext dbContext) : base(dbContext)
        {
            _appDbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<List<ResultHouseWithHouseTypeDto>> GetHouseWithHouseTypeAsync()
        {

            var values = await _appDbContext.Houses
            .Include(h => h.HouseTypes) // İlişkili HouseType bilgilerini dahil ediyoruz
            .Select(h => new ResultHouseWithHouseTypeDto
            {
                Bathrooms = h.Bathrooms,
                Bedrooms = h.Bedrooms,
                CreatedAt = h.CreatedAt,
                Description = h.Description,
                Garage = h.Garage,
                Garden = h.Garden,
                HeatingType = h.HeatingType,
                HouseId = h.HouseId,
                HouseTypeName = h.HouseTypes.Name,
                Location = h.Location,
                Pool = h.Pool,
                Price = h.Price,
                Size = h.Size,
                Title = h.Title,
                YearBuilt = h.YearBuilt,
            }).ToListAsync();
            

            return values;
        }
    }
}

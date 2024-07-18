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

        public async Task<List<ResultHousesWithNames>> GetHousesByHouseTypeId(int id)
        {
            var values = await _appDbContext.Houses
            .Include(x => x.HouseTypes)
            .Include(y => y.Owner)
            .Where(z => z.HouseTypeId == id)
            .Select(h => new ResultHousesWithNames
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
                OwnerName = h.Owner.Username,
                Location = h.Location,
                Pool = h.Pool,
                Price = h.Price,
                Size = h.Size,
                Title = h.Title,
                YearBuilt = h.YearBuilt,
            }).ToListAsync();


            return values;
        }

        public async Task<List<ResultHousesWithNames>> GetHouseWithNamesAsync()
        {

            var values = await _appDbContext.Houses
            .Include(x=>x.HouseTypes)
            .Include(y => y.Owner)
            .Select(h => new ResultHousesWithNames
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
                OwnerName = h.Owner.Username,
                Location = h.Location,
                Pool = h.Pool,
                Price = h.Price,
                Size = h.Size,
                Title = h.Title,
                YearBuilt = h.YearBuilt,
            }).ToListAsync();
            

            return values;
        }

        public async Task<ResultHousesWithNames> GetHouseWithNamesByIdAsync(int id)
        {
            var values = await _appDbContext.Houses
                .Include(x => x.HouseTypes)
                .Include(y => y.Owner)
                .Where(z => z.HouseId == id)
                .Select(h => new ResultHousesWithNames
                {
                    HouseId = id,
                    Bathrooms = h.Bathrooms,
                    Bedrooms = h.Bedrooms,
                    CreatedAt = h.CreatedAt,
                    Description = h.Description,
                    Garage = h.Garage,
                    Garden = h.Garden,
                    HeatingType = h.HeatingType,
                    HouseTypeName = h.HouseTypes.Name,
                    OwnerName = h.Owner.Username,
                    Location = h.Location,
                    Pool = h.Pool,
                    Price = h.Price,
                    Size = h.Size,
                    Title = h.Title,
                    YearBuilt = h.YearBuilt,
                    UpdatedAt = h.UpdatedAt
                }).FirstOrDefaultAsync();
            return values;
        }
    }
}

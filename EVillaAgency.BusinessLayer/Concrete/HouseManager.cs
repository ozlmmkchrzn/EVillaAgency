using EVillaAgency.BusinessLayer.Abstract;
using EVillaAgency.DataAccessLayer.Context;
using EVillaAgency.DtoLayer.FavoriteDtos;
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
            .Include(x => x.HouseType)
            .Include(y => y.Owner)
            .Include(a => a.HouseImages)
            .ThenInclude(ai => ai.Image)
            .Where(z => z.HouseTypeId == id)
            .Select(h => new ResultHousesWithNames
            {
                Bathrooms = h.Bathrooms,
                Bedrooms = h.Bedrooms,
                CreatedAt = h.CreatedAt,
                Description = h.Description,
                Garage = h.Garage,
                Garden = h.Garden,
                HeatingType = h.HeatingType.Name,
                HouseId = h.HouseId,
                HouseTypeName = h.HouseType.Name,
                OwnerName = h.Owner.Username,
                CityName = h.District.City.Name,
                DictrictName = h.District.Name,
                ImageUrl = h.HouseImages.Select(hi => hi.Image.Url).FirstOrDefault(),
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
            .Include(x => x.HouseType)
            .Include(y => y.Owner)
            .Include(h => h.HouseImages)
            .ThenInclude(a => a.Image)
            .Include(z => z.HeatingType)
            .Include(a => a.District)
            .ThenInclude(b => b.City)
            .Select(h => new ResultHousesWithNames
            {
                Bathrooms = h.Bathrooms,
                Bedrooms = h.Bedrooms,
                CreatedAt = h.CreatedAt,
                Description = h.Description,
                Garage = h.Garage,
                Garden = h.Garden,
                HeatingType = h.HeatingType.Name,
                HouseId = h.HouseId,
                HouseTypeName = h.HouseType.Name,
                OwnerName = h.Owner.Username,
                CityName = h.District.City.Name,
                DictrictName = h.District.Name,
                ImageUrl = h.HouseImages.Select(hi => hi.Image.Url).FirstOrDefault(),
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
                .Include(x => x.HouseType)
            .Include(y => y.Owner)
            .Include(z => z.HeatingType)
            .Include(a => a.District)
            .ThenInclude(b => b.City)
                .Where(z => z.HouseId == id)
                .Select(h => new ResultHousesWithNames
                {
                    Bathrooms = h.Bathrooms,
                    Bedrooms = h.Bedrooms,
                    CreatedAt = h.CreatedAt,
                    Description = h.Description,
                    Garage = h.Garage,
                    Garden = h.Garden,
                    HeatingType = h.HeatingType.Name,
                    HouseId = h.HouseId,
                    HouseTypeName = h.HouseType.Name,
                    OwnerName = h.Owner.Username,
                    CityName = h.District.City.Name,
                    DictrictName = h.District.Name,
                    ImageUrl = h.HouseImages.Select(hi => hi.Image.Url).FirstOrDefault(),
                    Location = h.Location,
                    Pool = h.Pool,
                    Price = h.Price,
                    Size = h.Size,
                    Title = h.Title,
                    YearBuilt = h.YearBuilt,
                }).FirstOrDefaultAsync();
            return values;
        }

        public async Task<List<ResultHousesWithNames>> GetLast6Houses()
        {
            var values = await _appDbContext.Houses
            .OrderByDescending(k => k.HouseId)
            .Take(6)
            .Include(x => x.HouseType)
            .Include(y => y.Owner)
            .Include(z => z.HeatingType)
            .Include(a => a.District)
            .ThenInclude(b => b.City)
            .Select(h => new ResultHousesWithNames
            {
                Bathrooms = h.Bathrooms,
                Bedrooms = h.Bedrooms,
                CreatedAt = h.CreatedAt,
                Description = h.Description,
                Garage = h.Garage,
                Garden = h.Garden,
                HeatingType = h.HeatingType.Name,
                HouseId = h.HouseId,
                HouseTypeName = h.HouseType.Name,
                OwnerName = h.Owner.Username,
                CityName = h.District.City.Name,
                DictrictName = h.District.Name,
                ImageUrl = h.HouseImages.Select(hi => hi.Image.Url).FirstOrDefault(),
                Location = h.Location,
                Pool = h.Pool,
                Price = h.Price,
                Size = h.Size,
                Title = h.Title,
                YearBuilt = h.YearBuilt,
            }).ToListAsync();


            return values;
        }

        public async Task<int> GetTotalHouseOwnerCount()
        {
            var values = await _appDbContext.Houses
                .Select(h => h.OwnerId)
                .Distinct()
                .CountAsync();
            return values;
        }

        public async Task<int> GetTotalHousesCountAsync()
        {
            try
            {
                return await _appDbContext.Houses.CountAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting total houses count: {ex.Message}", ex);
            }
        }
    }
}

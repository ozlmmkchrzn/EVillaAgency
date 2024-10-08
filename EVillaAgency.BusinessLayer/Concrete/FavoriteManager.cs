﻿using EVillaAgency.BusinessLayer.Abstract;
using EVillaAgency.DataAccessLayer.Context;
using EVillaAgency.DtoLayer.FavoriteDtos;
using EVillaAgency.EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVillaAgency.BusinessLayer.Concrete
{
    public class FavoriteManager : GenericRepository<Favorite>, IFavoriteService
    {
        private readonly AppDbContext _appDbContext;

        public FavoriteManager(AppDbContext dbContext) : base(dbContext)
        {
            _appDbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<int> GetFavoritedHouseCountAsync()
        {
            var count = await _appDbContext.Favorites
                .Select(x => x.HouseId)
                .Distinct()
                .CountAsync();
            return count;
        }

        public async Task<List<ResultAllFavoritesWithNamesDto>> GetAllFavoritesWithNamesAsync()
        {
            var values = await _appDbContext.Favorites
                .Include(x => x.House)
                .Include(y => y.AppUser)
                .Select(z => new ResultAllFavoritesWithNamesDto
                {
                    FavoriteId = z.FavoriteId,
                    HouseTitle = z.House.Title,
                    Username = z.AppUser.Name
                }).ToListAsync();
            return values;
        }

        public async Task<ResultHouseNameAndUsernameByFavoriteIdDto> GetHouseNameAndUserNameByFavoriteIdAsync(int id)
        {
            var values = await _appDbContext.Favorites
                .Include(x => x.House)
                .Include(y => y.AppUser)
                .Where(a => a.FavoriteId == id)
                .Select(z => new ResultHouseNameAndUsernameByFavoriteIdDto
                {
                    FavoriteId = id,
                    HouseName = z.House.Title,
                    UserName = z.AppUser.Name
                }).FirstOrDefaultAsync();
            return values;
        }

        public async Task<ResultTop3FavoritedHousesDto> GetMostFavoritedHouseAsync()
        {
            var topHouses = await _appDbContext.Favorites
            .GroupBy(f => f.HouseId)
            .Select(group => new
            {
                HouseId = group.Key,
                FavoriteCount = group.Count()
            })
            .OrderByDescending(g => g.FavoriteCount)
            .Take(1)
            .Join(_appDbContext.Houses,
                  g => g.HouseId,
                  h => h.HouseId,
                  (g, h) => new ResultTop3FavoritedHousesDto
                  {
                      HouseId = h.HouseId,
                      Title = h.Title,
                      Description = h.Description,
                      Price = h.Price,
                      Location = h.Location,
                      FavoriteCount = g.FavoriteCount,
                      Bathrooms = h.Bathrooms,
                      Bedrooms = h.Bedrooms,
                      CityName = h.District.City.Name,
                      DictrictName = h.District.Name,
                      Garage = h.Garage,
                      Garden = h.Garden,
                      HeatingTypeName = h.HeatingType.Name,
                      HouseTypeName = h.HouseType.Name,
                      OwnerName = h.Owner.Name,
                      Pool = h.Pool,
                      Size = h.Size,
                      YearBuilt = h.YearBuilt

                  })
            .FirstOrDefaultAsync();

            return topHouses;
        }

        

        public async Task<List<ResultTop3FavoritedHousesDto>> GetTopFavoritedHousesAsync()
        {
            var topHouses = await _appDbContext.Favorites
            .GroupBy(f => f.HouseId)
            .Select(group => new
            {
                HouseId = group.Key,
                FavoriteCount = group.Count()
            })
            .OrderByDescending(g => g.FavoriteCount)
            .Take(3)
            .Join(_appDbContext.Houses,
                  g => g.HouseId,
                  h => h.HouseId,
                  (g, h) => new ResultTop3FavoritedHousesDto
                  {
                      HouseId = h.HouseId,
                      Title = h.Title,
                      Description = h.Description,
                      Price = h.Price,
                      Location = h.Location,
                      FavoriteCount = g.FavoriteCount,
                      Bathrooms = h.Bathrooms,
                      Bedrooms = h.Bedrooms,
                      CityName = h.District.City.Name,
                      DictrictName = h.District.Name,
                      Garage = h.Garage,
                      Garden = h.Garden,
                      HeatingTypeName = h.HeatingType.Name,
                      HouseTypeName = h.HouseType.Name,
                      OwnerName = h.Owner.Name,
                      Pool = h.Pool,
                      Size = h.Size,
                      YearBuilt = h.YearBuilt
                      
                  })
            .ToListAsync();

            return topHouses;
        }
    }
}

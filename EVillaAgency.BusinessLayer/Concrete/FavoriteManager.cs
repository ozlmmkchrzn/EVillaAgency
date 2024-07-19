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

        public async Task<List<ResultAllFavoritesWithNamesDto>> GetAllFavoritesWithNamesAsync()
        {
            var values = await _appDbContext.Favorites
                .Include(x => x.House)
                .Include(y => y.User)
                .Select(z => new ResultAllFavoritesWithNamesDto
                {
                    FavoriteId = z.FavoriteId,
                    HouseTitle = z.House.Title,
                    Username = z.User.Username
                }).ToListAsync();
            return values;
        }

        public async Task<ResultHouseNameAndUsernameByFavoriteIdDto> GetHouseNameAndUserNameByFavoriteIdAsync(int id)
        {
            var values = await _appDbContext.Favorites
                .Include(x => x.House)
                .Include(y => y.User)
                .Where(a => a.FavoriteId == id)
                .Select(z => new ResultHouseNameAndUsernameByFavoriteIdDto
                {
                    FavoriteId = id,
                    HouseName = z.House.Title,
                    UserName = z.User.Username
                }).FirstOrDefaultAsync();
            return values;
        }
    }
}

using EVillaAgency.BusinessLayer.Abstract;
using EVillaAgency.DataAccessLayer.Context;
using EVillaAgency.DtoLayer.BasketDtos;
using EVillaAgency.EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVillaAgency.BusinessLayer.Concrete
{
    public class BasketManager : GenericRepository<Basket>, IBasketService
    {
        private readonly AppDbContext _appDbContext;
        public BasketManager(AppDbContext dbContext) : base(dbContext)
        {
            _appDbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task CreateBasket(CreateBasketDto dto)
        {
            try
            {
                // Create a new Basket object
                var basket = new Basket
                {
                    HouseId = dto.HouseId,
                    UserId = dto.UserId,
                    CreatedDate = DateTime.Now,
                    Status = true
                };

                // Add the new Basket to the database context
                await _appDbContext.Baskets.AddAsync(basket);

                // Save changes to the database
                await _appDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Handle or log the exception as needed
                // For example: log the exception, rethrow it, or handle it based on your application's requirements
                throw new ApplicationException("An error occurred while creating the basket.", ex);
            }

        }

        public async Task<List<ResultBasketDto>> GetAllBAskets()
        {
            var values = await _appDbContext.Baskets
                .Include(x => x.House)
                .Include( y => y.User)
                .Select(h => new ResultBasketDto
                {
                    BasketId = h.BasketId,
                    HouseTitle = h.House.Title,
                    HousePrice = h.House.Price,
                    HouseTypeName = h.House.HouseType.Name,
                    OwnerName = h.House.Owner.Username,
                    UserName = h.User.Username,
                }).ToListAsync();
            return values;
        }

        public async Task<ResultBasketDto> GetBasketById(int id)
        {
            var values = await _appDbContext.Baskets
                .Include( x=> x.House)
                .Include( y => y.User)
                .Where(z => z.BasketId == id)
                .Select(h => new ResultBasketDto {
                    BasketId = h.BasketId,
                    HousePrice = h.House.Price,
                    HouseTitle= h.House.Title,
                    HouseTypeName= h.House.HouseType.Name,
                    OwnerName= h.House.Owner.Username,
                    UserName = h.User.Username
                }).FirstOrDefaultAsync();
            return values;
        }

        public async Task<ResultBasketDto> GetLastBasketByUserId(int id)
        {
            var values = await _appDbContext.Baskets
                .Include(x => x.House)
                .Include(y => y.User)
                .Where(z => z.UserId == id)
                .OrderByDescending(z => z.BasketId)
                .Take(1)
                .Select(h => new ResultBasketDto
                {
                    BasketId = h.BasketId,
                    HousePrice = h.House.Price,
                    HouseTitle = h.House.Title,
                    HouseTypeName = h.House.HouseType.Name,
                    OwnerName = h.House.Owner.Username,
                    UserName = h.User.Username
                }).FirstOrDefaultAsync();
            return values;
        }
    }
}

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
                    UserId = 1,
                    CreatedDate = DateTime.Now
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
    }
}

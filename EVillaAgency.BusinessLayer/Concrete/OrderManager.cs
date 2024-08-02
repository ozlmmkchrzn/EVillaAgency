using EVillaAgency.BusinessLayer.Abstract;
using EVillaAgency.DataAccessLayer.Context;
using EVillaAgency.DtoLayer.OrderDtos;
using EVillaAgency.EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVillaAgency.BusinessLayer.Concrete
{
    public class OrderManager : GenericRepository<Order>, IOrderService
    {
        private readonly AppDbContext _appDbContext;
        public OrderManager(AppDbContext dbContext) : base(dbContext)
        {
            _appDbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task CreateOrder(CreateOrderDto dto)
        {
            var basket = await _appDbContext.Baskets
                .Include(b => b.House)
                .Include(b => b.User)
                .FirstOrDefaultAsync(b => b.BasketId == dto.BasketId);

            
            Order order = new Order
            {
                BasketId = dto.BasketId,
                CreatedDate = DateTime.Now,
            };

            basket.House.OwnerId = basket.UserId;

            basket.House.Status = false;
            basket.Status = false;

            _appDbContext.Houses.Update(basket.House);

            await _appDbContext.Orders.AddAsync(order);

            await _appDbContext.SaveChangesAsync();
        }
    }
}

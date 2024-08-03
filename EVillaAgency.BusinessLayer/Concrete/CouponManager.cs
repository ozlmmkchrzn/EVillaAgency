using EVillaAgency.BusinessLayer.Abstract;
using EVillaAgency.DataAccessLayer.Context;
using EVillaAgency.DtoLayer.CouponDtos;
using EVillaAgency.EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVillaAgency.BusinessLayer.Concrete
{
    public class CouponManager : GenericRepository<Coupon>, ICouponService
    {
        private readonly AppDbContext _appDbContext;
        public CouponManager(AppDbContext dbContext) : base(dbContext)
        {
            _appDbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<bool> CheckCouponAsync(string coupon)
        {
            var values = await _appDbContext.Coupons
                .Where(x=>x.Code == coupon && x.StartedDate< DateTime.Now && x.EndedDate > DateTime.Now).FirstOrDefaultAsync();
            if (values == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public async Task CreateCoupon(CreateCouponDto dto)
        {
            Coupon coupon = new Coupon
            {
                Code = dto.Code,
                DiscountRate = dto.DiscountRate,
                EndedDate = dto.EndedDate,
                StartedDate = dto.StartedDate,
                Status = true
            };
            await _appDbContext.AddAsync(coupon);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task UpdateCoupon(UpdateCouponDto dto)
        {
            var values = await _appDbContext.Coupons
                .Where(z => z.CouponId == dto.CouponId)
                .FirstOrDefaultAsync();

            values.Code = dto.Code;
            values.DiscountRate = dto.DiscountRate;
            values.EndedDate = dto.EndedDate;
            values.StartedDate = dto.StartedDate;

            _appDbContext.Update(values);

            await _appDbContext.SaveChangesAsync();
        }

        public async Task UpdateExpiredCouponsAsync()
        {
            var expiredCoupons = await _appDbContext.Coupons
            .Where(c => c.EndedDate < DateTime.Now && c.Status == true)
            .ToListAsync();

            foreach (var coupon in expiredCoupons)
            {
                coupon.Status = false;
            }

            await _appDbContext.SaveChangesAsync();
        }
    }
}

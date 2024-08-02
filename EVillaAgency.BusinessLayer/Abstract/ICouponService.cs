using EVillaAgency.DtoLayer.CouponDtos;
using EVillaAgency.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVillaAgency.BusinessLayer.Abstract
{
    public interface ICouponService : IGenericService<Coupon>
    {
        Task UpdateCoupon(UpdateCouponDto dto);

        Task CreateCoupon(CreateCouponDto dto);

        Task UpdateExpiredCouponsAsync();

        Task<bool> ApplyCoupon(string couponcode);
    }
}

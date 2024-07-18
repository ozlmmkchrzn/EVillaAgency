using EVillaAgency.BusinessLayer.Abstract;
using EVillaAgency.DataAccessLayer.Context;
using EVillaAgency.DtoLayer.HouseImageDtos;
using EVillaAgency.EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVillaAgency.BusinessLayer.Concrete
{
    public class HouseImageManager : GenericRepository<HouseImage>, IHouseImageService
    {
        private readonly AppDbContext _appDbContext;
        public HouseImageManager(AppDbContext dbContext) : base(dbContext)
        {
            _appDbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<ResultHouseImageWithNamesDto> GetHouseAndImageNamesByHouseImageId(int id)
        {
            var values = await _appDbContext.HouseImages
                .Include(x => x.House)
                .Include(y => y.Image)
                .Where(z => z.Id == id)
                .Select(z => new ResultHouseImageWithNamesDto
                {
                    Id = id,
                    HouseName = z.House.Title,
                    ImageUrl = z.Image.Url
                }).FirstOrDefaultAsync();
            return values;
        }

        public async Task<List<ResultHouseImageWithNamesDto>> GetImagesWithNames()
        {
            var values = await _appDbContext.HouseImages
                .Include(x => x.House)
                .Include(y => y.Image)
                .Select(z => new ResultHouseImageWithNamesDto
                {
                    Id = z.Id,
                    HouseName = z.House.Title,
                    ImageUrl = z.Image.Url
                }).ToListAsync();
            return values;
        }
    }
}

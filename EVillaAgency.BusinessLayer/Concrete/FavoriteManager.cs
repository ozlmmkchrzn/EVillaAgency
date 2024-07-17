﻿using EVillaAgency.BusinessLayer.Abstract;
using EVillaAgency.DataAccessLayer.Context;
using EVillaAgency.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVillaAgency.BusinessLayer.Concrete
{
    public class FavoriteManager : GenericRepository<Favorite>, IFavoriteService
    {
        public FavoriteManager(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVillaAgency.EntityLayer.Concrete
{
    public class Image
    {
        public int Id { get; set; }
        public string Url { get; set; }

        public ICollection<HouseImage> HouseImages { get; set; }
    }
}

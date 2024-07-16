﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVillaAgency.EntityLayer.Concrete
{
    public class HouseImage
    {
        public int Id { get; set; }
        public int HouseId { get; set; }
        public int ImageId { get; set; }

        public House House { get; set; }
        public Image Image { get; set; }

    }
}

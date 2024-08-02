﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVillaAgency.DtoLayer.CouponDtos
{
    public class CreateCouponDto
    {
        public string Code { get; set; }
        public DateTime StartedDate { get; set; }
        public DateTime EndedDate { get; set; }
        public float DiscountRate { get; set; }
    }
}

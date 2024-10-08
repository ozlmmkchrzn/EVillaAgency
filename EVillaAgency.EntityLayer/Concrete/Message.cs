﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVillaAgency.EntityLayer.Concrete
{
    public class Message
    {
        public int Id { get; set; }
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public int HouseId { get; set; }
        public string Content { get; set; }
        public DateTime SentAt { get; set; }

        public AppUser Sender { get; set; }
        public AppUser Receiver { get; set; }
        public House House { get; set; }

    }
}

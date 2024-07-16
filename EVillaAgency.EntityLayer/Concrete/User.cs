using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVillaAgency.EntityLayer.Concrete
{
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string ImageUrl { get; set; }
        public DateTime CreatedAt { get; set; }

        public ICollection<House> Houses { get; set; }
        //public ICollection<Message> SentMessages { get; set; }
        //public ICollection<Message> ReceivedMessages { get; set; }
        public ICollection<Favorite> Favorites { get; set; }

    }
}

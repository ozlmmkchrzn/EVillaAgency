using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVillaAgency.EntityLayer.Concrete
{
    public class AppUser : IdentityUser<int>
    {
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public DateTime CreatedAt { get; set; }

        public ICollection<House> Houses { get; set; }
        //public ICollection<Message> SentMessages { get; set; }
        //public ICollection<Message> ReceivedMessages { get; set; }
        public ICollection<Favorite> Favorites { get; set; }

    }
}

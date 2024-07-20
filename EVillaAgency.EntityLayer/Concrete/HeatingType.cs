using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVillaAgency.EntityLayer.Concrete
{
    public class HeatingType
    {
        public int HeatingTypeId { get; set; }
        public string Name { get; set; }

        public ICollection<House> Houses { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Intra.Practice.Engineering.Models
{
    public class DayOff
    {
        public DateTime start { get; set; }
        public DateTime end { get; set; }
        public String reason { get; set; }
        public String state { get; set; }
        public String id { get; set; }
        public String emailOwner { get; set; }
    }
}

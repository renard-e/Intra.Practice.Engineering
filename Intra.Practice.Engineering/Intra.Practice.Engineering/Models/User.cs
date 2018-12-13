using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Intra.Practice.Engineering.Models
{
    public class User
    {
        [Key]
        public String email { get; set; }
        public String password { get; set; }
        public String group { get; set; }
    }
}

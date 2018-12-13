using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Intra.Practice.Engineering.Models;

namespace Intra.Practice.Engineering.Data
{
    public static class IntraInitializer
    {
        public static void Initialize(IntraContext context)
        {
            context.Database.EnsureCreated();

            if (context.Users.Any())
                return;
        }
    }
}

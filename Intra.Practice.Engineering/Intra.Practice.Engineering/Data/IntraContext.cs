using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Intra.Practice.Engineering.Models;
using Microsoft.EntityFrameworkCore;

namespace Intra.Practice.Engineering.Data
{
    public class IntraContext : DbContext
    {
        public IntraContext(DbContextOptions<IntraContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<DayOff> DayOffs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<DayOff>().ToTable("DayOff");
        }
    }
}

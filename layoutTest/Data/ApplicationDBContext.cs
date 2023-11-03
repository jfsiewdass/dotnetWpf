using layoutTest.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace layoutTest.Data
{
    public class ApplicationDBContext : DbContext
    {
        public DbSet<VoterModel> Voters { get; set; }

        public ApplicationDBContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<VoterModel>();//.HasKey(e => e.Id);
        }
    }
}

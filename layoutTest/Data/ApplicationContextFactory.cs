using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace layoutTest.Data
{

    public class ApplicationContextFactory : IDesignTimeDbContextFactory<ApplicationDBContext>
    {
        public ApplicationDBContext CreateDbContext(string[] args = null)
        {
            var options = new DbContextOptionsBuilder<ApplicationDBContext>();
            options.UseSqlServer("Server=192.168.4.66;Database=CRUD_USERS;User=sa;Password=Abc123456;TrustServerCertificate=true;MultipleActiveResultSets=true");
            return new ApplicationDBContext(options.Options);
        }
    }
}

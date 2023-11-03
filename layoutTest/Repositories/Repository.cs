using layoutTest.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace layoutTest.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationContextFactory _contextFactory;
        private readonly NonqueryDataService<T> _nonQueryDataService;

        public Repository(ApplicationContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
            _nonQueryDataService = new NonqueryDataService<T>(contextFactory);

        }


        public async Task<T> Create(T entity)
        {

            return await _nonQueryDataService.Create(entity);

        }

        public async Task<bool> Delete(T entity)
        {

            return await _nonQueryDataService.Delete(entity);

        }

        public async Task<T> Get(int id)
        {
            using (var transaction = _contextFactory.CreateDbContext().Database.BeginTransaction())
            {
                using ApplicationDBContext context = _contextFactory.CreateDbContext();
                return await context.Set<T>().FindAsync(id);
            }
        }


        public async Task<IEnumerable<T>> GetAll()
        {
            using ApplicationDBContext context = _contextFactory.CreateDbContext();
            return await context.Set<T>().ToListAsync();

        }

        public async Task<T> Update(T entity)
        {

            return await _nonQueryDataService.Update(entity);
        }

        public ApplicationDBContext Bd()
        {
            ApplicationDBContext context = _contextFactory.CreateDbContext();
            return context;
        }
    }
}


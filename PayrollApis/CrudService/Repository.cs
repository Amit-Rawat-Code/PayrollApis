
using Microsoft.EntityFrameworkCore;
using PayrollApis.Models;
using System.Threading.Tasks;

namespace PayrollApis.CrudService
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly SqlDbContext _sqlDbContext;
        private readonly DbSet<T> _dbSet;
        public Repository(SqlDbContext sqlDbContext)
        {
            this._sqlDbContext = sqlDbContext;
            this._dbSet = sqlDbContext.Set<T>();
        }
        public async Task<T> AddAsync(T entity) 
        {
            await _dbSet.AddAsync(entity);
            await _sqlDbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity == null) return false;
            _dbSet.Remove(entity);
            await _sqlDbContext.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<T>> GetAllAsync() => await _dbSet.ToListAsync();


        public async Task<T?> GetByIdAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity == null) return null;
            else
            return entity;
        }

        public async Task<T> UpdateAsync(T entity) 
        {
            _dbSet.Update(entity);
            await _sqlDbContext.SaveChangesAsync();
            return entity;
        }
        public async Task<bool> EntityExists(int id)
        {
            return await _sqlDbContext.Employees.AnyAsync(e => e.EmployeeId == id);
        }
    }
}

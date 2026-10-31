using Microsoft.EntityFrameworkCore;
using PayrollApis.CrudService;
using PayrollApis.Models;

namespace PayrollApis.ModelServices
{
    public class DesignationServices
    {
        private readonly IRepository<Designation> repository;
        private readonly SqlDbContext sqlDbContext;

        public DesignationServices(IRepository<Designation> repository,SqlDbContext sqlDbContext)
        {
            this.repository = repository;
            this.sqlDbContext = sqlDbContext;
        }

        public async Task<IEnumerable<Designation>> GetAllDesignations()
        {
            return await repository.GetAllAsync();
        }
        public async Task<Designation?> GetDesignationById(int id)
        {
            return await repository.GetByIdAsync(id);
        }
        public async Task<Designation> AddDesigantion(Designation desn)
        {
            var checkUnique = await sqlDbContext.Designations.AnyAsync(e => e.DesignationName == desn.DesignationName);
            if (checkUnique)
                throw new Exception("Record alraedy exists");
            return await repository.AddAsync(desn);
        }
        public async Task<Designation> UpdateDesigantion(Designation desn)
        {
            return await repository.UpdateAsync(desn);
        }
        public async Task<bool> DeleteDesignation(int id)
        {
            return await repository.DeleteAsync(id);
        }
        public async Task<bool> IsDesignationExists(int id)
        {
            return await repository.EntityExists(id);
        }
    }
}

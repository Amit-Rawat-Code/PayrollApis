using Microsoft.EntityFrameworkCore;
using PayrollApis.CrudService;
using PayrollApis.Models;

namespace PayrollApis.ModelServices
{
    public class DepartmentServices
    {
        private readonly IRepository<Department> repository;
        private readonly SqlDbContext sqlDbContext;

        public DepartmentServices(IRepository<Department> repository,SqlDbContext sqlDbContext)
        {
            this.repository = repository;
            this.sqlDbContext = sqlDbContext;
        }

        public async Task<IEnumerable<Department>> GetAllDepartments()
        {
            return await repository.GetAllAsync();
        }
        public async Task<Department?> GetDepartmentById(int id)
        {
            return await repository.GetByIdAsync(id);
        }
        public async Task<Department> AddDepartment(Department dept)
        {
            var checkUnique = await sqlDbContext.Departments.AnyAsync(e => e.DepartmentName == dept.DepartmentName);
            if (checkUnique)
                throw new Exception("Record alraedy exists");
            return await repository.AddAsync(dept);
        }
        public async Task<Department> UpdateDepartment(Department dept)
        {
            return await repository.UpdateAsync(dept);
        }
        public async Task<bool> DeleteDepartment(int id)
        {
            return await repository.DeleteAsync(id);
        }
        public async Task<bool> IsDepartmentExists(int id)
        {
            return await repository.EntityExists(id);
        }

    }
}

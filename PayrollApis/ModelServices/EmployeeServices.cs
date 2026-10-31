using Microsoft.EntityFrameworkCore;
using PayrollApis.CrudService;
using PayrollApis.Models;

namespace PayrollApis.ModelServices
{
    public class EmployeeServices
    {
        private readonly IRepository<Employee> repository;
        private readonly SqlDbContext sqlDbContext;

        public EmployeeServices(IRepository<Employee> repository,SqlDbContext sqlDbContext)
        {
            this.repository = repository;
            this.sqlDbContext = sqlDbContext;
        }
        public async Task<IEnumerable<Employee>> GetAllEmployees()
        {
            return await repository.GetAllAsync();
        }
        public async Task<Employee?> GetEmployeeById(int id) 
        {
            return await repository.GetByIdAsync(id);
        }
        public async Task<Employee> AddEmployee(Employee emp)
        {
            var checkUnique = await sqlDbContext.Employees.AnyAsync(e => e.EmpCode == emp.EmpCode);
            if (checkUnique)
                throw new Exception("Record alraedy exists");
            return await repository.AddAsync(emp);
        }
        public async Task<Employee> UpdateEmployee(Employee emp)
        {
            return await repository.UpdateAsync(emp);
        }
        public async Task<bool> DeleteEmployee(int id)
        {
            return await repository.DeleteAsync(id);
        }
        public async Task<bool> IsEmployeeExists(int id)
        {
            return await repository.EntityExists(id);
        }
    }
}

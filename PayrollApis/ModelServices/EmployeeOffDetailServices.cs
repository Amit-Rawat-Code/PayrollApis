using Microsoft.EntityFrameworkCore;
using PayrollApis.CrudService;
using PayrollApis.DtosModels;
using PayrollApis.Models;

namespace PayrollApis.ModelServices
{
    public class EmployeeOffDetailServices
    {
        private readonly IRepository<EmployeeOfficialDetail> repository;
        private readonly SqlDbContext sqlDbContext;

        public EmployeeOffDetailServices(IRepository<EmployeeOfficialDetail> repository, SqlDbContext sqlDbContext)
        {
            this.repository = repository;
            this.sqlDbContext = sqlDbContext;
        }
        public async Task<IEnumerable<EmployeeDto>> GetEmployeeOfficialDetailList()
        {
            var result = from empOff in sqlDbContext.EmployeeOfficialDetails

                         join emp in sqlDbContext.Employees
                         on empOff.EmployeeId equals emp.EmployeeId
                         into empGroup
                         from emp in empGroup.DefaultIfEmpty()

                         join dept in sqlDbContext.Departments
                         on empOff.DepartmentId equals dept.DepartmentId
                         into deptGroup
                         from dept in deptGroup.DefaultIfEmpty()

                         join desn in sqlDbContext.Designations
                         on empOff.DesignationId equals desn.DesignationId
                         into desnGroup
                         from desn in desnGroup.DefaultIfEmpty()

                         select new EmployeeDto
                         {
                             EmpOfficeId = empOff.EmpOfficeId,
                             EmployeeId = empOff.EmployeeId,
                             DepartmentId = empOff.DepartmentId,
                             DesignationId = empOff.DesignationId,
                             EmployeeName = emp != null ? emp.EmployeeName : null,
                             DepartmentName = dept != null ? dept.DepartmentName : null,
                             DesignationName = desn != null ? desn.DesignationName : null,
                             EmploymentType = empOff.EmploymentType,
                             DOJ = empOff.DOJ,
                             ReportingManagerId = empOff.ReportingManagerId
                         };
            return await result.ToListAsync();

        }
        public async Task<EmployeeOfficialDetail?> GetEmployeeOfficialDetailById(int id)
        {
            return await repository.GetByIdAsync(id);
        }
        public async Task<EmployeeOfficialDetail> AddEmployeeOfficialDetail(EmployeeOfficialDetail empOff)
        {
            var checkUnique = await sqlDbContext.EmployeeOfficialDetails.AnyAsync(e => e.EmployeeId == empOff.EmployeeId);
            if (checkUnique)
                return null;
            return await repository.AddAsync(empOff);
        }
        public async Task<EmployeeOfficialDetail> UpdateEmployeeOfficialDetail(int id, EmployeeOfficialDetail empOff)
        {
            var empOffDetail = await sqlDbContext.EmployeeOfficialDetails
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.EmpOfficeId == id);
            if (empOffDetail == null)
                return null;
            return await repository.UpdateAsync(empOff);
        }
        public async Task<bool> DeleteEmployeeOfficialDetail(int id)
        {
            var empOffDetail = await sqlDbContext.EmployeeOfficialDetails.FindAsync(id);
            if (empOffDetail == null)
                return false;
            return await repository.DeleteAsync(id);
        }
        public async Task<List<ReportingManagerDto>> GetReportingHierarchy()
        {
            var result = await sqlDbContext.Set<ReportingManagerDto>()
            .FromSqlRaw(@"WITH EmployeeHierarchy AS (
                            SELECT emp.EmployeeId, EmployeeName, ReportingManagerId
                            FROM Employees emp JOIN EmployeeOfficialDetails eod on emp.EmployeeId = eod.EmployeeId
                            WHERE ReportingManagerId = 0 
                            UNION ALL 
                            SELECT em.EmployeeId, em.EmployeeName, em.ReportingManagerId FROM (
                            SELECT e.EmployeeId, e.EmployeeName, ed.ReportingManagerId 
                            FROM Employees e JOIN EmployeeOfficialDetails ed on e.EmployeeId = ed.EmployeeId) em 
                            JOIN EmployeeHierarchy h 
                            ON em.ReportingManagerId = h.EmployeeId 
                            ) 
                            SELECT * FROM EmployeeHierarchy;")
            .ToListAsync();

            return result;
        }
    }
}

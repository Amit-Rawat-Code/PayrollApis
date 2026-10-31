using Microsoft.EntityFrameworkCore;
using PayrollApis.CrudService;
using PayrollApis.DtosModels;
using PayrollApis.Models;

namespace PayrollApis.ModelServices
{
    public class EmployeeStatuaryServices
    {
        private readonly SqlDbContext sqlDbContext;
        private readonly IRepository<EmployeeStatuary> repository;

        public EmployeeStatuaryServices(SqlDbContext sqlDbContext,IRepository<EmployeeStatuary> repository)
        {
            this.sqlDbContext = sqlDbContext;
            this.repository = repository;
        }
        public async Task<IEnumerable<EmployeeStatuaryDto>> GetEmployeeStatuaryList()
        {
            var empStatuaryList = from empstat in sqlDbContext.EmployeeStatuarys
                                  join emp in sqlDbContext.Employees
                                  on empstat.EmployeeId equals emp.EmployeeId
                                  into empStatGroup
                                  from emp in empStatGroup.DefaultIfEmpty()

                                  select new EmployeeStatuaryDto
                                  {
                                      StatutoryId = empstat.StatutoryId,
                                      EmployeeId = empstat.EmployeeId,
                                      EmployeeName = emp.EmployeeName,
                                      IsPfApplicable = empstat.IsPfApplicable,
                                      IsEsiApplicable = empstat.IsEsiApplicable,
                                      UAN = empstat.UAN,
                                      PFAccountNo = empstat.PFAccountNo,
                                      EsiNo = empstat.EsiNo,
                                      EffectiveFrom = empstat.EffectiveFrom,
                                      IsActive = empstat.IsActive,
                                      CreatedOn = empstat.CreatedOn,
                                      CreatedBy = empstat.CreatedBy,
                                      UpdatedOn = empstat.UpdatedOn,
                                      UpdatedBy = empstat.UpdatedBy
                                  };
            return await empStatuaryList.ToListAsync();
        }
        public async Task<EmployeeStatuaryDto?> GetEmployeeStatuaryById(int id)
        {
            var empStatuary = from empStat in sqlDbContext.EmployeeStatuarys
                              join emp in sqlDbContext.Employees
                              on empStat.EmployeeId equals emp.EmployeeId

                              select new EmployeeStatuaryDto
                              {
                                  StatutoryId = empStat.StatutoryId,
                                  EmployeeId = empStat.EmployeeId,
                                  EmployeeName = emp.EmployeeName,
                                  IsPfApplicable = empStat.IsPfApplicable,
                                  IsEsiApplicable = empStat.IsEsiApplicable,
                                  PFAccountNo = empStat.PFAccountNo,
                                  EsiNo = empStat.EsiNo,
                                  UAN = empStat.UAN,
                                  EffectiveFrom = empStat.EffectiveFrom,
                                  IsActive = empStat.IsActive,
                                  CreatedOn = empStat.CreatedOn,
                                  CreatedBy = empStat.CreatedBy,
                                  UpdatedOn = empStat.UpdatedOn,
                                  UpdatedBy = empStat.UpdatedBy
                              };
            return await empStatuary.SingleOrDefaultAsync();
        }
        public async Task<EmployeeStatuaryDto?> AddEmployeeStatuary(EmployeeStatuary employeeStatuary)
        {
            var exists = await sqlDbContext.EmployeeStatuarys.AnyAsync(e=>e.EmployeeId == employeeStatuary.EmployeeId);
            if (exists)
                return null;
            employeeStatuary.CreatedBy = employeeStatuary.CreatedBy == null ? 1 : employeeStatuary.CreatedBy;
            await repository.AddAsync(employeeStatuary);

            var empStatuary = from empStat in sqlDbContext.EmployeeStatuarys
                              join emp in sqlDbContext.Employees
                              on empStat.EmployeeId equals emp.EmployeeId
                              where empStat.EmployeeId == employeeStatuary.EmployeeId

                              select new EmployeeStatuaryDto
                              {
                                  StatutoryId = empStat.StatutoryId,
                                  EmployeeId = empStat.EmployeeId,
                                  EmployeeName = emp.EmployeeName,
                                  IsPfApplicable = empStat.IsPfApplicable,
                                  IsEsiApplicable = empStat.IsEsiApplicable,
                                  PFAccountNo = empStat.PFAccountNo,
                                  EsiNo = empStat.EsiNo,
                                  UAN = empStat.UAN,
                                  EffectiveFrom = empStat.EffectiveFrom,
                                  IsActive = empStat.IsActive,
                                  CreatedOn = empStat.CreatedOn,
                                  CreatedBy = empStat.CreatedBy,
                                  UpdatedOn = empStat.UpdatedOn,
                                  UpdatedBy = empStat.UpdatedBy
                              };
            return await empStatuary.SingleOrDefaultAsync();
        }
        public async Task<EmployeeStatuaryDto?> UpdateEmployeeStatuary(int id,EmployeeStatuary employeeStatuary)
        {
            var statuary = await sqlDbContext.EmployeeStatuarys.AsNoTracking().FirstOrDefaultAsync(e => e.StatutoryId == id);
            if (statuary == null)
                return null;
            statuary.IsPfApplicable = employeeStatuary.IsPfApplicable;
            statuary.IsEsiApplicable = employeeStatuary.IsEsiApplicable;
            statuary.PFAccountNo = employeeStatuary.PFAccountNo;
            statuary.EsiNo = employeeStatuary.EsiNo;
            statuary.UAN = employeeStatuary.UAN;
            statuary.EffectiveFrom = employeeStatuary.EffectiveFrom;
            statuary.UpdatedOn = DateTime.Now;
            statuary.UpdatedBy = statuary.UpdatedBy == null ? 1 : statuary.UpdatedBy;

            await repository.UpdateAsync(statuary);

            var empStatuary = from empStat in sqlDbContext.EmployeeStatuarys
                              join emp in sqlDbContext.Employees
                              on empStat.EmployeeId equals emp.EmployeeId
                              into empStatGroup
                              from emp in empStatGroup.DefaultIfEmpty()

                              select new EmployeeStatuaryDto
                              {
                                  StatutoryId = empStat.StatutoryId,
                                  EmployeeId = empStat.EmployeeId,
                                  EmployeeName = emp.EmployeeName,
                                  IsPfApplicable = empStat.IsPfApplicable,
                                  IsEsiApplicable = empStat.IsEsiApplicable,
                                  PFAccountNo = empStat.PFAccountNo,
                                  EsiNo = empStat.EsiNo,
                                  UAN = empStat.UAN,
                                  EffectiveFrom = empStat.EffectiveFrom,
                                  IsActive = empStat.IsActive,
                                  CreatedOn = empStat.CreatedOn,
                                  CreatedBy = empStat.CreatedBy,
                                  UpdatedOn = empStat.UpdatedOn,
                                  UpdatedBy = empStat.UpdatedBy
                              };
            return await empStatuary.SingleOrDefaultAsync();
        }
        public async Task<bool> DeleteEmployeeStatuary(int id)
        {
            var statuary = await sqlDbContext.EmployeeStatuarys.FindAsync(id);
            if(statuary == null)
                return false;
            return await repository.DeleteAsync(id);
        }        
    }
}

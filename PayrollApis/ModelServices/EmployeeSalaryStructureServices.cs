using Microsoft.EntityFrameworkCore;
using PayrollApis.CrudService;
using PayrollApis.DtosModels;
using PayrollApis.Models;

namespace PayrollApis.ModelServices
{
    public class EmployeeSalaryStructureServices
    {
        private readonly SqlDbContext sqlDbContext;
        private readonly IRepository<EmployeeSalaryStructure> repository;

        public EmployeeSalaryStructureServices(SqlDbContext sqlDbContext,IRepository<EmployeeSalaryStructure> repository)
        {
            this.sqlDbContext = sqlDbContext;
            this.repository = repository;
        }
        public async Task<IEnumerable<EmployeeSalaryStructureDto>> GetEmployeesSalaryStructureList()
        {
            var empSalStructList = from empSalStruct in sqlDbContext.EmployeeSalaryStructures
                                   join emp in sqlDbContext.Employees
                                   on empSalStruct.EmployeeId equals emp.EmployeeId
                                   into empSalStructGroup
                                   from emp in empSalStructGroup.DefaultIfEmpty()

                                   select new EmployeeSalaryStructureDto
                                   {
                                       SalaryStructureId = empSalStruct.SalaryStructureId,
                                       EmployeeId = empSalStruct.EmployeeId,
                                       EmployeeName = emp.EmployeeName,
                                       Basic = empSalStruct.Basic,
                                       ConveyanceAllowance = empSalStruct.ConveyanceAllowance,
                                       DA = empSalStruct.DA,
                                       DailyRate = empSalStruct.DailyRate,
                                       HourlyRate = empSalStruct.HourlyRate,
                                       HRA = empSalStruct.HRA,
                                       MedicalAllowance = empSalStruct.MedicalAllowance,
                                       SalaryType = empSalStruct.SalaryType,
                                       SpecialAllowance = empSalStruct.SpecialAllowance,
                                       WorkingDaysPerMonth = empSalStruct.WorkingDaysPerMonth,
                                       EffectiveFrom = empSalStruct.EffectiveFrom,
                                       IsActive = empSalStruct.IsActive,
                                       CreatedOn = empSalStruct.CreatedOn,
                                       CreatedBy = empSalStruct.CreatedBy,
                                       UpdatedBy = empSalStruct.UpdatedBy,
                                       UpdatedOn = empSalStruct.UpdatedOn,
                                   };
            return await empSalStructList.ToListAsync();
        }
        public async Task<EmployeeSalaryStructureDto?> GetEmployeeSalaryStructureById(int id)
        {
            var empSalStructDetail =     from empSalStruct in sqlDbContext.EmployeeSalaryStructures
                                   join emp in sqlDbContext.Employees
                                   on empSalStruct.EmployeeId equals emp.EmployeeId
                                   where empSalStruct.SalaryStructureId == id

                                   select new EmployeeSalaryStructureDto
                                   {
                                       SalaryStructureId = empSalStruct.SalaryStructureId,
                                       EmployeeId = empSalStruct.EmployeeId,
                                       EmployeeName = emp.EmployeeName,
                                       Basic = empSalStruct.Basic,
                                       ConveyanceAllowance = empSalStruct.ConveyanceAllowance,
                                       DA = empSalStruct.DA,
                                       DailyRate = empSalStruct.DailyRate,
                                       HourlyRate = empSalStruct.HourlyRate,
                                       HRA = empSalStruct.HRA,
                                       MedicalAllowance = empSalStruct.MedicalAllowance,
                                       SalaryType = empSalStruct.SalaryType,
                                       SpecialAllowance = empSalStruct.SpecialAllowance,
                                       WorkingDaysPerMonth = empSalStruct.WorkingDaysPerMonth,
                                       EffectiveFrom = empSalStruct.EffectiveFrom,
                                       IsActive = empSalStruct.IsActive,
                                       CreatedOn = empSalStruct.CreatedOn,
                                       CreatedBy = empSalStruct.CreatedBy,
                                       UpdatedBy = empSalStruct.UpdatedBy,
                                       UpdatedOn = empSalStruct.UpdatedOn,
                                   };
            return await empSalStructDetail.SingleOrDefaultAsync();
        }
        public async Task<EmployeeSalaryStructureDto?> AddEmployeeSalaryStructure(EmployeeSalaryStructure employeeSalaryStructure)
        {
            var exists = await sqlDbContext.EmployeeSalaryStructures.AnyAsync(e=>e.EmployeeId == employeeSalaryStructure.EmployeeId);
            if (exists)
                return null;
            employeeSalaryStructure.CreatedBy = employeeSalaryStructure.CreatedBy == null ? 1 : employeeSalaryStructure.CreatedBy;

            await repository.AddAsync(employeeSalaryStructure);

            var empSalStructDetail = from empSalStruct in sqlDbContext.EmployeeSalaryStructures
                                     join emp in sqlDbContext.Employees
                                     on empSalStruct.EmployeeId equals emp.EmployeeId
                                     where empSalStruct.SalaryStructureId == employeeSalaryStructure.EmployeeId

                                     select new EmployeeSalaryStructureDto
                                     {
                                         SalaryStructureId = empSalStruct.SalaryStructureId,
                                         EmployeeId = empSalStruct.EmployeeId,
                                         EmployeeName = emp.EmployeeName,
                                         Basic = empSalStruct.Basic,
                                         ConveyanceAllowance = empSalStruct.ConveyanceAllowance,
                                         DA = empSalStruct.DA,
                                         DailyRate = empSalStruct.DailyRate,
                                         HourlyRate = empSalStruct.HourlyRate,
                                         HRA = empSalStruct.HRA,
                                         MedicalAllowance = empSalStruct.MedicalAllowance,
                                         SalaryType = empSalStruct.SalaryType,
                                         SpecialAllowance = empSalStruct.SpecialAllowance,
                                         WorkingDaysPerMonth = empSalStruct.WorkingDaysPerMonth,
                                         EffectiveFrom = empSalStruct.EffectiveFrom,
                                         IsActive = empSalStruct.IsActive,
                                         CreatedOn = empSalStruct.CreatedOn,
                                         CreatedBy = empSalStruct.CreatedBy,
                                         UpdatedBy = empSalStruct.UpdatedBy,
                                         UpdatedOn = empSalStruct.UpdatedOn,
                                     };
            return await empSalStructDetail.SingleOrDefaultAsync();
        }
        public async Task<EmployeeSalaryStructureDto?> UpdateEmployeeSalaryStructure(int id,EmployeeSalaryStructure employeeSalaryStructure)
        {
            var salaryStructures = await sqlDbContext.EmployeeSalaryStructures.AsNoTracking().FirstOrDefaultAsync(e => e.SalaryStructureId == id);
            if (salaryStructures == null)
                return null;

            salaryStructures.Basic = employeeSalaryStructure.Basic;
            salaryStructures.ConveyanceAllowance = employeeSalaryStructure.ConveyanceAllowance;
            salaryStructures.DA = employeeSalaryStructure.DA;
            salaryStructures.DailyRate = employeeSalaryStructure.DailyRate;
            salaryStructures.HourlyRate = employeeSalaryStructure.HourlyRate;
            salaryStructures.HRA = employeeSalaryStructure.HRA;
            salaryStructures.MedicalAllowance = employeeSalaryStructure.MedicalAllowance;
            salaryStructures.SalaryType = employeeSalaryStructure.SalaryType;
            salaryStructures.SpecialAllowance = employeeSalaryStructure.SpecialAllowance;
            salaryStructures.WorkingDaysPerMonth = employeeSalaryStructure.WorkingDaysPerMonth;
            salaryStructures.EffectiveFrom = employeeSalaryStructure.EffectiveFrom;
            salaryStructures.IsActive = employeeSalaryStructure.IsActive;
            salaryStructures.UpdatedOn = DateTime.Now;
            salaryStructures.UpdatedBy = employeeSalaryStructure.UpdatedBy == null ? 1 : employeeSalaryStructure.UpdatedBy;

            await repository.UpdateAsync(salaryStructures);
            var empSalStructDetail = from empSalStruct in sqlDbContext.EmployeeSalaryStructures
                                     join emp in sqlDbContext.Employees
                                     on empSalStruct.EmployeeId equals emp.EmployeeId
                                     where empSalStruct.SalaryStructureId == id

                                     select new EmployeeSalaryStructureDto
                                     {
                                         SalaryStructureId = empSalStruct.SalaryStructureId,
                                         EmployeeId = empSalStruct.EmployeeId,
                                         EmployeeName = emp.EmployeeName,
                                         Basic = empSalStruct.Basic,
                                         ConveyanceAllowance = empSalStruct.ConveyanceAllowance,
                                         DA = empSalStruct.DA,
                                         DailyRate = empSalStruct.DailyRate,
                                         HourlyRate = empSalStruct.HourlyRate,
                                         HRA = empSalStruct.HRA,
                                         MedicalAllowance = empSalStruct.MedicalAllowance,
                                         SalaryType = empSalStruct.SalaryType,
                                         SpecialAllowance = empSalStruct.SpecialAllowance,
                                         WorkingDaysPerMonth = empSalStruct.WorkingDaysPerMonth,
                                         EffectiveFrom = empSalStruct.EffectiveFrom,
                                         IsActive = empSalStruct.IsActive,
                                         CreatedOn = empSalStruct.CreatedOn,
                                         CreatedBy = empSalStruct.CreatedBy,
                                         UpdatedBy = empSalStruct.UpdatedBy,
                                         UpdatedOn = empSalStruct.UpdatedOn,
                                     };
            return await empSalStructDetail.SingleOrDefaultAsync();
        }
        public async Task<bool> DeleteEmployeeSalaryStructure(int id)
        {
            var salaryStructures = await sqlDbContext.EmployeeSalaryStructures.FindAsync(id);
            if (salaryStructures == null)
                return false;
            return await repository.DeleteAsync(id);
        }        
    }
}

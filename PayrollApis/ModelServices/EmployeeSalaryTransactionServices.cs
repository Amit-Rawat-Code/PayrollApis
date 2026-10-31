using Microsoft.EntityFrameworkCore;
using PayrollApis.CrudService;
using PayrollApis.DtosModels;
using PayrollApis.Helper;
using PayrollApis.Models;
using System.Threading.Tasks;

namespace PayrollApis.ModelServices
{
    public class EmployeeSalaryTransactionServices
    {
        private readonly SqlDbContext sqlDbContext;
        private readonly IRepository<EmployeeSalaryTransaction> repository;

        public EmployeeSalaryTransactionServices(SqlDbContext sqlDbContext, IRepository<EmployeeSalaryTransaction> repository)
        {
            this.sqlDbContext = sqlDbContext;
            this.repository = repository;
        }
        public async Task<IEnumerable<EmployeeSalaryTransactionDto>> GetEmployeesSalaryTransactionList()
        {
            var employeeSalTransList = from empSalTrans in sqlDbContext.EmployeeSalaryTransactions
                                       join emp in sqlDbContext.Employees
                                       on empSalTrans.EmployeeId equals emp.EmployeeId
                                       into empSalTranGroup
                                       from emp in empSalTranGroup.DefaultIfEmpty()

                                       select new EmployeeSalaryTransactionDto
                                       {
                                           SalaryTransactionId = empSalTrans.SalaryTransactionId,
                                           EmployeeId = empSalTrans.EmployeeId,
                                           EmployeeName = emp.EmployeeName,
                                           Month = empSalTrans.Month,
                                           Year = empSalTrans.Year,
                                           PF = empSalTrans.PF,
                                           ESI = empSalTrans.ESI,
                                           PresentDays = empSalTrans.PresentDays,
                                           TotalHours = empSalTrans.TotalHours,
                                           ProfessionalTax = empSalTrans.ProfessionalTax,
                                           TotalDeduction = empSalTrans.TotalDeduction,
                                           GrossSalary = empSalTrans.GrossSalary,
                                           NetSalary = empSalTrans.NetSalary,
                                           ProcessedOn = empSalTrans.ProcessedOn,
                                           IsActive = empSalTrans.IsActive,
                                           CreatedOn = empSalTrans.CreatedOn,
                                           CreatedBy = empSalTrans.CreatedBy,
                                           UpdatedBy = empSalTrans.UpdatedBy,
                                           UpdatedOn = empSalTrans.UpdatedOn,
                                       };
            return await employeeSalTransList.ToListAsync();
        }

        public async Task<IEnumerable<EmployeeSalaryTransactionDto>> GetEmployeesSalaryTransactionListDepartmentWise(int deptId,int month,int year)
        {
            var employeeSalTransList = from emp in sqlDbContext.Employees.AsNoTracking()
                                       join ss in sqlDbContext.EmployeeSalaryStructures.AsNoTracking()
                                           on emp.EmployeeId equals ss.EmployeeId
                                       join eod in sqlDbContext.EmployeeOfficialDetails.AsNoTracking()
                                           on emp.EmployeeId equals eod.EmployeeId
                                       join sal in sqlDbContext.EmployeeSalaryTransactions
                                           .Where(s => s.Month == month && s.Year == year)
                                           on emp.EmployeeId equals sal.EmployeeId into salGroup
                                       from sal in salGroup.DefaultIfEmpty()

                                       where eod.IsActive == true
                                           && ss.IsActive == true
                                           && eod.DepartmentId == deptId

                                       select new EmployeeSalaryTransactionDto
                                       {
                                           EmployeeId = emp.EmployeeId,
                                           //EmployeeCode = emp.EmpCode,
                                           EmployeeName = emp.EmployeeName,
                                           SalaryType = ss.SalaryType,

                                           Month = sal != null ? sal.Month : 0,
                                           Year = sal != null ? sal.Year : 0,
                                           PresentDays = sal != null ? sal.PresentDays : 0,
                                           TotalHours = sal != null ? sal.TotalHours : 0,
                                           PF = sal != null ? sal.PF : 0,
                                           ESI = sal != null ? sal.ESI : 0,
                                           ProfessionalTax = sal != null ? sal.ProfessionalTax : 0,
                                           GrossSalary = sal != null ? sal.GrossSalary : 0,
                                           TotalDeduction = sal != null ? sal.TotalDeduction : 0,
                                           NetSalary = sal != null ? sal.NetSalary : 0,
                                           ProcessedOn = sal != null ? sal.ProcessedOn : null,
                                           CreatedBy = sal != null ? sal.CreatedBy ?? 0 : 0
                                       };
            return await employeeSalTransList.ToListAsync();
        }
        public async Task<EmployeeSalaryTransactionDto?> GetEmployeesSalaryTransactionById(int id)
        {
            var SalaryTrans = await sqlDbContext.EmployeeSalaryTransactions.FindAsync(id);
            if (SalaryTrans == null)
                return null;
            var employeeSalTrans = from empSalTrans in sqlDbContext.EmployeeSalaryTransactions
                                   join emp in sqlDbContext.Employees
                                   on empSalTrans.EmployeeId equals emp.EmployeeId

                                   select new EmployeeSalaryTransactionDto
                                   {
                                       SalaryTransactionId = empSalTrans.SalaryTransactionId,
                                       EmployeeId = empSalTrans.EmployeeId,
                                       EmployeeName = emp.EmployeeName,
                                       Month = empSalTrans.Month,
                                       Year = empSalTrans.Year,
                                       PF = empSalTrans.PF,
                                       ESI = empSalTrans.ESI,
                                       PresentDays = empSalTrans.PresentDays,
                                       TotalHours = empSalTrans.TotalHours,
                                       ProfessionalTax = empSalTrans.ProfessionalTax,
                                       TotalDeduction = empSalTrans.TotalDeduction,
                                       GrossSalary = empSalTrans.GrossSalary,
                                       NetSalary = empSalTrans.NetSalary,
                                       ProcessedOn = empSalTrans.ProcessedOn,
                                       IsActive = empSalTrans.IsActive,
                                       CreatedOn = empSalTrans.CreatedOn,
                                       CreatedBy = empSalTrans.CreatedBy,
                                       UpdatedBy = empSalTrans.UpdatedBy,
                                       UpdatedOn = empSalTrans.UpdatedOn,
                                   };
            return await employeeSalTrans.SingleOrDefaultAsync();
        }
        public async Task<EmployeeSalaryTransactionDto?> AddEmployeeSalaryTranaction(EmployeeSalaryTransaction employeeSalaryTransaction)
        {
            var exists = await sqlDbContext.EmployeeSalaryTransactions.AnyAsync(
                e => e.EmployeeId == employeeSalaryTransaction.EmployeeId &&
                e.Month == employeeSalaryTransaction.Month &&
                e.Year == employeeSalaryTransaction.Year);
            if (exists)
                return null;

            employeeSalaryTransaction.CreatedBy = employeeSalaryTransaction.CreatedBy == null ? 1 : employeeSalaryTransaction.CreatedBy;

            await repository.AddAsync(employeeSalaryTransaction);
            var employeeSalTrans = from empSalTrans in sqlDbContext.EmployeeSalaryTransactions
                                   join emp in sqlDbContext.Employees
                                   on empSalTrans.EmployeeId equals emp.EmployeeId
                                   where empSalTrans.EmployeeId == employeeSalaryTransaction.EmployeeId

                                   select new EmployeeSalaryTransactionDto
                                   {
                                       SalaryTransactionId = empSalTrans.SalaryTransactionId,
                                       EmployeeId = empSalTrans.EmployeeId,
                                       EmployeeName = emp.EmployeeName,
                                       Month = empSalTrans.Month,
                                       Year = empSalTrans.Year,
                                       PF = empSalTrans.PF,
                                       ESI = empSalTrans.ESI,
                                       PresentDays = empSalTrans.PresentDays,
                                       TotalHours = empSalTrans.TotalHours,
                                       ProfessionalTax = empSalTrans.ProfessionalTax,
                                       TotalDeduction = empSalTrans.TotalDeduction,
                                       GrossSalary = empSalTrans.GrossSalary,
                                       NetSalary = empSalTrans.NetSalary,
                                       ProcessedOn = empSalTrans.ProcessedOn,
                                       IsActive = empSalTrans.IsActive,
                                       CreatedOn = empSalTrans.CreatedOn,
                                       CreatedBy = empSalTrans.CreatedBy,
                                       UpdatedBy = empSalTrans.UpdatedBy,
                                       UpdatedOn = empSalTrans.UpdatedOn,
                                   };
            return await employeeSalTrans.SingleOrDefaultAsync();
        }
        public async Task<EmployeeSalaryTransactionDto?> UpdateEmployeeSalaryTranaction(int id, EmployeeSalaryTransaction employeeSalaryTransaction)
        {
            var salaryTrans = await sqlDbContext.EmployeeSalaryTransactions.AsNoTracking().FirstOrDefaultAsync(e=>e.SalaryTransactionId == id);
            if (salaryTrans == null)
                return null;

            salaryTrans.Month = employeeSalaryTransaction.Month;
            salaryTrans.Year = employeeSalaryTransaction.Year;
            salaryTrans.PF = employeeSalaryTransaction.PF;
            salaryTrans.ESI = employeeSalaryTransaction.ESI;
            salaryTrans.PresentDays = employeeSalaryTransaction.PresentDays;
            salaryTrans.TotalHours = employeeSalaryTransaction.TotalHours;
            salaryTrans.ProfessionalTax = employeeSalaryTransaction.ProfessionalTax;
            salaryTrans.TotalDeduction = employeeSalaryTransaction.TotalDeduction;
            salaryTrans.GrossSalary = employeeSalaryTransaction.GrossSalary;
            salaryTrans.NetSalary = employeeSalaryTransaction.NetSalary;
            salaryTrans.ProcessedOn = employeeSalaryTransaction.ProcessedOn;
            salaryTrans.IsActive = employeeSalaryTransaction.IsActive;
            salaryTrans.UpdatedOn = DateTime.Now;
            salaryTrans.UpdatedBy = salaryTrans.UpdatedBy == null ? 1 : salaryTrans.UpdatedBy;

            await repository.UpdateAsync(salaryTrans);
            var employeeSalTrans = from empSalTrans in sqlDbContext.EmployeeSalaryTransactions
                                   join emp in sqlDbContext.Employees
                                   on empSalTrans.EmployeeId equals emp.EmployeeId
                                   where empSalTrans.SalaryTransactionId == employeeSalaryTransaction.SalaryTransactionId

                                   select new EmployeeSalaryTransactionDto
                                   {
                                       SalaryTransactionId = empSalTrans.SalaryTransactionId,
                                       EmployeeId = empSalTrans.EmployeeId,
                                       EmployeeName = emp.EmployeeName,
                                       Month = empSalTrans.Month,
                                       Year = empSalTrans.Year,
                                       PF = empSalTrans.PF,
                                       ESI = empSalTrans.ESI,
                                       PresentDays = empSalTrans.PresentDays,
                                       TotalHours = empSalTrans.TotalHours,
                                       ProfessionalTax = empSalTrans.ProfessionalTax,
                                       TotalDeduction = empSalTrans.TotalDeduction,
                                       GrossSalary = empSalTrans.GrossSalary,
                                       NetSalary = empSalTrans.NetSalary,
                                       ProcessedOn = empSalTrans.ProcessedOn,
                                       IsActive = empSalTrans.IsActive,
                                       CreatedOn = empSalTrans.CreatedOn,
                                       CreatedBy = empSalTrans.CreatedBy,
                                       UpdatedBy = empSalTrans.UpdatedBy,
                                       UpdatedOn = empSalTrans.UpdatedOn,
                                   };
            return await employeeSalTrans.SingleOrDefaultAsync();
        }
        public async Task<bool> DeleteEmployeeSalaryTranaction(int id)
        {
            var salaryTrans = await sqlDbContext.EmployeeSalaryTransactions.FindAsync(id);
            if (salaryTrans == null)
                return false;
            return await repository.DeleteAsync(id);
        }
        public async Task<List<EmployeeStatuaryDto?>> GetAllStauariesByIds(List<int> empIds)
        {
            var statuaries = from empStatuary in sqlDbContext.EmployeeStatuarys
                             where empIds.Contains(empStatuary.EmployeeId)
                             select new EmployeeStatuaryDto
                             {
                                 StatutoryId = empStatuary.StatutoryId,
                                 IsEsiApplicable = empStatuary.IsEsiApplicable,
                                 IsPfApplicable = empStatuary.IsPfApplicable,                                 
                             };
            return await statuaries.ToListAsync();
        }
        public async Task<List<EmployeeSalaryStructureDto?>> GetAllSalaryStructresByIds(List<int> empIds)
        {
            var salaryStructs = from empsalStruct in sqlDbContext.EmployeeSalaryStructures
                                where empIds.Contains(empsalStruct.EmployeeId)
                                select new EmployeeSalaryStructureDto
                                {
                                    SalaryStructureId = empsalStruct.SalaryStructureId,
                                    Basic = empsalStruct.Basic,
                                    DA = empsalStruct.DA,
                                    HRA = empsalStruct.HRA,
                                    ConveyanceAllowance = empsalStruct.ConveyanceAllowance,
                                    MedicalAllowance = empsalStruct.MedicalAllowance,
                                    SpecialAllowance = empsalStruct.SpecialAllowance,
                                    WorkingDaysPerMonth = empsalStruct.WorkingDaysPerMonth,
                                };
            return await salaryStructs.ToListAsync();
        }

        public async Task<List<EmployeeSalaryTransactionDto>> CalculateSalary(List<EmployeeSalaryTransactionDto> salaryList, int year, int month)
        {
            int daysInMonth = DateTime.DaysInMonth(year, month);

            // 1. Get all employee IDs
            List<int> employeeIds = salaryList.Select(x => x.EmployeeId).ToList();

            // 2. Fetch data in bulk (IMPORTANT)
            var salaries = await GetAllSalaryStructresByIds(employeeIds);
            var statList = await GetAllStauariesByIds(employeeIds);

            // 3. Convert to dictionary for fast lookup
            var salaryDict = salaries
                            .GroupBy(x => x.EmployeeId)
                            .ToDictionary(g => g.Key, g => g.First());

            var statDict = statList
                           .GroupBy(x => x.EmployeeId)
                           .ToDictionary(g => g.Key, g => g.First());


            // 4. Process data
            foreach (var emp in salaryList)
            {
                // Set defaults using switch expression
                (int presentDays, float totalHours) = emp.SalaryType switch
                {
                    (int)SalaryType.Monthly => (daysInMonth, 0),
                    (int)SalaryType.Daily => (daysInMonth, 0),
                    (int)SalaryType.Hourly => (daysInMonth, daysInMonth * 8),
                    _ => (0, 0)
                };

                emp.PresentDays = presentDays;
                emp.TotalHours = totalHours;

                // Get salary & statutory using dictionary (O(1))
                salaryDict.TryGetValue(emp.EmployeeId, out var salary);
                statDict.TryGetValue(emp.EmployeeId, out var statutory);

                // Null safety
                if (salary == null || statutory == null)
                {
                    emp.GrossSalary = 0;
                    emp.NetSalary = 0;
                    continue;
                }

                // Calculate payroll
                var result = PayrollCalculator.Calculate(
                    salary,
                    statutory,
                    presentDays,
                    totalHours,
                    month,
                    year
                );

                // Assign results
                emp.GrossSalary = result.GrossSalary;
                emp.PF = result.PF;
                emp.ESI = result.ESI;
                emp.ProfessionalTax = result.ProfessionalTax;
                emp.NetSalary = result.NetSalary;
            }

            return salaryList;
        }
    }
    public enum SalaryType
    {
        Monthly = 1,
        Daily = 2,
        Hourly = 3
    }
}

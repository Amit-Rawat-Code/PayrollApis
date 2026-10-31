using Microsoft.EntityFrameworkCore;
using PayrollApis.CrudService;
using PayrollApis.DtosModels;
using PayrollApis.Models;

namespace PayrollApis.ModelServices
{
    public class EmployeeBankDetailServices
    {
        private readonly SqlDbContext sqlDbContext;
        private readonly IRepository<EmployeeBankDetail> repository;

        public EmployeeBankDetailServices(SqlDbContext sqlDbContext,IRepository<EmployeeBankDetail> repository)
        {
            this.sqlDbContext = sqlDbContext;
            this.repository = repository;
        }
        public async Task<IEnumerable<EmployeeBankDetailDto>> GetEmployeesBankDetailList()
        {
            var empBnkDetailList =       from ebd in sqlDbContext.EmployeeBankDetails
                                         join emp in sqlDbContext.Employees
                                         on ebd.EmployeeId equals emp.EmployeeId
                                         into empBnkGroup
                                         from emp in empBnkGroup.DefaultIfEmpty()

                                         select new EmployeeBankDetailDto
                                         {
                                             BankId = ebd.BankId,
                                             EmployeeId = ebd.EmployeeId,
                                             EmployeeName = emp.EmployeeName,
                                             Aadhaar = ebd.Aadhaar,
                                             AccountNo = ebd.AccountNo,
                                             BankName = ebd.BankName,
                                             IFSC = ebd.IFSC,
                                             PAN = ebd.PAN,
                                             PaymentMode = ebd.PaymentMode,
                                             IsActive = ebd.IsActive,
                                             CreatedOn = ebd.CreatedOn,
                                             CreatedBy = ebd.CreatedBy,
                                             UpdatedBy = ebd.UpdatedBy,
                                             UpdatedOn = ebd.UpdatedOn,
                                         };
            return await empBnkDetailList.ToListAsync();
        }

        public async Task<EmployeeBankDetailDto?> GetEmployeeBankDetailById(int id)
        {
            var empBnkDetail = from ebd in sqlDbContext.EmployeeBankDetails
                               join emp in sqlDbContext.Employees
                               on ebd.EmployeeId equals emp.EmployeeId

                               where ebd.BankId == id
                               select new EmployeeBankDetailDto
                               {
                                   BankId = ebd.BankId,
                                   EmployeeId = ebd.EmployeeId,
                                   EmployeeName = emp.EmployeeName,
                                   Aadhaar = ebd.Aadhaar,
                                   AccountNo = ebd.AccountNo,
                                   BankName = ebd.BankName,
                                   IFSC = ebd.IFSC,
                                   PAN = ebd.PAN,
                                   PaymentMode = ebd.PaymentMode,
                                   IsActive = ebd.IsActive,
                                   CreatedOn = ebd.CreatedOn,
                                   CreatedBy = ebd.CreatedBy,
                                   UpdatedBy = ebd.UpdatedBy,
                                   UpdatedOn = ebd.UpdatedOn,
                               };
            return await empBnkDetail.SingleOrDefaultAsync();
        }
        public async Task<EmployeeBankDetailDto?> AddEmployeeBankDetail(EmployeeBankDetail entity)
        {
            var exists = await sqlDbContext.EmployeeBankDetails.AnyAsync(e => e.EmployeeId == entity.EmployeeId);
            if (exists)
                return null;
            entity.CreatedBy = entity.CreatedBy == null ? 1 : entity.CreatedBy;
            await repository.AddAsync(entity);

            var empBnkDetail = from ebd in sqlDbContext.EmployeeBankDetails
                               join emp in sqlDbContext.Employees
                               on ebd.EmployeeId equals emp.EmployeeId
                               where ebd.EmployeeId == entity.EmployeeId

                               select new EmployeeBankDetailDto
                               {
                                   BankId = ebd.BankId,
                                   EmployeeId = ebd.EmployeeId,
                                   EmployeeName = emp.EmployeeName,
                                   Aadhaar = ebd.Aadhaar,
                                   AccountNo = ebd.AccountNo,
                                   BankName = ebd.BankName,
                                   IFSC = ebd.IFSC,
                                   PAN = ebd.PAN,
                                   PaymentMode = ebd.PaymentMode,
                                   IsActive = ebd.IsActive,
                                   CreatedOn = ebd.CreatedOn,
                                   CreatedBy = ebd.CreatedBy,
                                   UpdatedBy = ebd.UpdatedBy,
                                   UpdatedOn = ebd.UpdatedOn,
                               };
            return await empBnkDetail.SingleOrDefaultAsync();
        }
        public async Task<EmployeeBankDetailDto?> UpdateEmployeeBankDetail(int id,EmployeeBankDetail entity)
        {
            var empBnkDetail = await sqlDbContext.EmployeeBankDetails.AsNoTracking().FirstOrDefaultAsync(e => e.BankId == id);
            if (empBnkDetail == null)
                return null;

            empBnkDetail.BankId= entity.BankId;
            empBnkDetail.Aadhaar= entity.Aadhaar;
            empBnkDetail.BankName= entity.BankName;
            empBnkDetail.EmployeeId= entity.EmployeeId;
            empBnkDetail.IFSC= entity.IFSC;
            empBnkDetail.PAN= entity.PAN;
            empBnkDetail.PaymentMode= entity.PaymentMode;
            empBnkDetail.IsActive= entity.IsActive;
            empBnkDetail.UpdatedOn= DateTime.Now;
            empBnkDetail.UpdatedBy = entity.UpdatedBy == null ? 1 : entity.UpdatedBy;

            await repository.UpdateAsync(empBnkDetail);

            var empUpdatedBnkDetail = from ebd in sqlDbContext.EmployeeBankDetails
                               join emp in sqlDbContext.Employees
                               on ebd.EmployeeId equals emp.EmployeeId
                               where ebd.EmployeeId == entity.EmployeeId

                               select new EmployeeBankDetailDto
                               {
                                   BankId = ebd.BankId,
                                   EmployeeId = ebd.EmployeeId,
                                   EmployeeName = emp.EmployeeName,
                                   Aadhaar = ebd.Aadhaar,
                                   AccountNo = ebd.AccountNo,
                                   BankName = ebd.BankName,
                                   IFSC = ebd.IFSC,
                                   PAN = ebd.PAN,
                                   PaymentMode = ebd.PaymentMode,
                                   IsActive = ebd.IsActive,
                                   CreatedOn = ebd.CreatedOn,
                                   CreatedBy = ebd.CreatedBy,
                                   UpdatedBy = ebd.UpdatedBy,
                                   UpdatedOn = ebd.UpdatedOn,
                               };
            return await empUpdatedBnkDetail.SingleOrDefaultAsync();
        }
        public async Task<bool> DeleteEmployeeBankDetail(int id)
        {
            var empBnkDetail = await sqlDbContext.EmployeeBankDetails.FindAsync(id);
            if (empBnkDetail == null)
                return false;
            return await repository.DeleteAsync(id);
        }
    }
}

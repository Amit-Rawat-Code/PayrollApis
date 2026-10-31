using Microsoft.AspNetCore.Mvc;
using PayrollApis.DtosModels;
using PayrollApis.Models;
using PayrollApis.ModelServices;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PayrollApis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeSalaryTransactionController : BaseController
    {
        private readonly EmployeeSalaryTransactionServices employeeSalaryTransactionServices;

        public EmployeeSalaryTransactionController(EmployeeSalaryTransactionServices employeeSalaryTransactionServices)
        {
            this.employeeSalaryTransactionServices = employeeSalaryTransactionServices;
        }
        // GET: api/<EmployeeSalaryTransactionController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await employeeSalaryTransactionServices.GetEmployeesSalaryTransactionList();
            if (result == null)
                return ApiError("Record not found");
            return ApiSuccess(result, "Employee Salary Transaction List");
        }

        // GET api/<EmployeeSalaryTransactionController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await employeeSalaryTransactionServices.GetEmployeesSalaryTransactionById(id);
            if (result == null)
                return ApiError("Record not found");
            return ApiSuccess(result, "Employee Salary Transaction");
        }

        // POST api/<EmployeeSalaryTransactionController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] EmployeeSalaryTransaction values)
        {
            var result = await employeeSalaryTransactionServices.AddEmployeeSalaryTranaction(values);
            if (result == null)
                return ApiError("Already exists");
            return ApiSuccess(result, "Added successfully");
        }

        // PUT api/<EmployeeSalaryTransactionController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] EmployeeSalaryTransaction values)
        {
            var result = await employeeSalaryTransactionServices.UpdateEmployeeSalaryTranaction(id, values);
            if (result == null)
                return ApiError("Record not found");
            return ApiSuccess(result, "Updated successfully");
        }

        // DELETE api/<EmployeeSalaryTransactionController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await employeeSalaryTransactionServices.DeleteEmployeeSalaryTranaction(id);
            if (!result)
                return ApiError("Record not found");
            return ApiSuccess(result, "Deleted Successfully");
        }
        [HttpPost("calculate-salary")]
        public async Task<IActionResult> Post([FromBody] List<EmployeeSalaryTransactionDto> empSalTransList, [FromQuery] int month, [FromQuery] int year)
        {
            var result = await employeeSalaryTransactionServices.CalculateSalary(empSalTransList, month, year);
            return ApiSuccess(result, "Calculated Salary List");
        }
        [HttpGet("department-salary")]
        public async Task<IActionResult> GetSalary([FromQuery] int deptId,int month,int year)
        {
            var result = await employeeSalaryTransactionServices.GetEmployeesSalaryTransactionListDepartmentWise(deptId, month, year);
            return ApiSuccess(result, "employee salary transaction list");
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using PayrollApis.DtosModels;
using PayrollApis.Models;
using PayrollApis.ModelServices;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PayrollApis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeBankDetailController : BaseController
    {
        private readonly EmployeeBankDetailServices employeeBankDetailServices;

        public EmployeeBankDetailController(EmployeeBankDetailServices employeeBankDetailServices)
        {
            this.employeeBankDetailServices = employeeBankDetailServices;
        }
        // GET: api/<EmployeeBankDetailController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await employeeBankDetailServices.GetEmployeesBankDetailList();
            if (result == null)
                return ApiError("Employee's Bank Detail Not found");
            return ApiSuccess(result, "Employee's Bank Detail List");
        }

        // GET api/<EmployeeBankDetailController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await employeeBankDetailServices.GetEmployeeBankDetailById(id);
            if (result == null)
                ApiError("Not found");
            return ApiSuccess(result, "Employee Bank Detail");
        }

        // POST api/<EmployeeBankDetailController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] EmployeeBankDetail values)
        {
            var result = await employeeBankDetailServices.AddEmployeeBankDetail(values);
            if (result == null)
                return ApiError("Already exists");
            return ApiSuccess(result, "Added successfully");
        }

        // PUT api/<EmployeeBankDetailController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] EmployeeBankDetail values)
        {
            var result = await employeeBankDetailServices.UpdateEmployeeBankDetail(id, values);
            if (result == null)
                return ApiError("Record not found");
            return ApiSuccess(result, "Updated successfully");
        }

        // DELETE api/<EmployeeBankDetailController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var IsDeleted = await employeeBankDetailServices.DeleteEmployeeBankDetail(id);
            if (IsDeleted)
                ApiError("Record not found");
            return ApiSuccess(IsDeleted, "Deleted successfully");
        }
    }
}

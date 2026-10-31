using Microsoft.AspNetCore.Mvc;
using PayrollApis.Models;
using PayrollApis.ModelServices;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PayrollApis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeSalaryStructureController : BaseController
    {
        private readonly EmployeeSalaryStructureServices employeeSalaryStructureServices;

        public EmployeeSalaryStructureController(EmployeeSalaryStructureServices employeeSalaryStructureServices)
        {
            this.employeeSalaryStructureServices = employeeSalaryStructureServices;
        }
        // GET: api/<EmployeeSalaryStructureController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await employeeSalaryStructureServices.GetEmployeesSalaryStructureList();
            if (result == null)
                return ApiError("Record not found");
            return ApiSuccess(result, "Salary structure list");
        }

        // GET api/<EmployeeSalaryStructureController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await employeeSalaryStructureServices.GetEmployeesSalaryStructureList();
            if (result == null)
                return ApiError("Record not found");
            return ApiSuccess(result, "Salary structure");
        }

        // POST api/<EmployeeSalaryStructureController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] EmployeeSalaryStructure values)
        {
            var result = await employeeSalaryStructureServices.AddEmployeeSalaryStructure(values);
            if (result == null)
                return ApiError("Already exists");
            return ApiSuccess(result, "Added successfully");
        }

        // PUT api/<EmployeeSalaryStructureController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] EmployeeSalaryStructure values)
        {
            var result = await employeeSalaryStructureServices.UpdateEmployeeSalaryStructure(id,values);
            if (result == null)
                return ApiError("Record not found");
            return ApiSuccess(result, "Updated successfully");
        }

        // DELETE api/<EmployeeSalaryStructureController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await employeeSalaryStructureServices.DeleteEmployeeSalaryStructure(id);
            if (!result)
                return ApiError("Record not found");
            return ApiSuccess(result, "Deleted successfully");
        }
    }
}

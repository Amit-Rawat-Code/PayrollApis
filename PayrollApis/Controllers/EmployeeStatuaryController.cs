using Microsoft.AspNetCore.Mvc;
using PayrollApis.Models;
using PayrollApis.ModelServices;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PayrollApis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeStatuaryController : BaseController
    {
        private readonly EmployeeStatuaryServices employeeStatuaryServices;

        public EmployeeStatuaryController(EmployeeStatuaryServices employeeStatuaryServices)
        {
            this.employeeStatuaryServices = employeeStatuaryServices;
        }
        // GET: api/<EmployeeStatuaryController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await employeeStatuaryServices.GetEmployeeStatuaryList();
            if (result == null)
                return ApiError("Employee Statuary list not found");
            return ApiSuccess(result, "Employee statuary list");
        }

        // GET api/<EmployeeStatuaryController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await employeeStatuaryServices.GetEmployeeStatuaryById(id);
            if (result == null)
                return ApiError("Employee Statuary not found");
            return ApiSuccess(result, "Employee statuary");
        }

        // POST api/<EmployeeStatuaryController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] EmployeeStatuary values)
        {
            var result = await employeeStatuaryServices.AddEmployeeStatuary(values);
            if (result == null)
                return ApiError("Already exists");
            return ApiSuccess(result, "Added Employee statuary successfully");
        }

        // PUT api/<EmployeeStatuaryController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] EmployeeStatuary values)
        {
            var result = await employeeStatuaryServices.UpdateEmployeeStatuary(id, values);
            if (result == null)
                return ApiError("Employee statuary not found to update");
            return ApiSuccess(result, "Updated employee statuary successfully");
        }

        // DELETE api/<EmployeeStatuaryController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await employeeStatuaryServices.DeleteEmployeeStatuary(id);
            if (!result)
                return ApiError("Employee statuary not found");
            return ApiSuccess(result, "Employee statuary deleted successfully");
        }
    }
}

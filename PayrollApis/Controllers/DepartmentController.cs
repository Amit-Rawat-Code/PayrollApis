using Microsoft.AspNetCore.Mvc;
using PayrollApis.Models;
using PayrollApis.ModelServices;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PayrollApis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : BaseController
    {
        private readonly DepartmentServices departmentServices;

        public DepartmentController(DepartmentServices departmentServices)
        {
            this.departmentServices = departmentServices;
        }
        // GET: api/<DepartmentController>
        [HttpGet]
        public async Task<IActionResult> GetDEpartments()
        {
            var departments= await departmentServices.GetAllDepartments();

            return ApiSuccess(departments, "Department fetched successfully");
        }

        // GET api/<DepartmentController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var dept = await departmentServices.GetDepartmentById(id);
            return ApiSuccess(dept,dept.DepartmentName);
        }

        // POST api/<DepartmentController>
        [HttpPost]
        public async Task<IActionResult> AddDepartment([FromBody] Department value)
        {
            var dept = await departmentServices.AddDepartment(value);
            return ApiSuccess(dept,"Department added");
        }

        // PUT api/<DepartmentController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Department value)
        {
            if(id != value.DepartmentId)
                return ApiError("Department Id mismatch");
            var dept = await departmentServices.UpdateDepartment(value);
            if (dept is null)
                return ApiError("Department not found");
            return ApiSuccess(dept,"Department updated");
        }

        // DELETE api/<DepartmentController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var dept = await departmentServices.DeleteDepartment(id);
            if (!dept)
                return ApiError("Department not found");
            return ApiSuccess(dept,"Department deleted");
        }
    }
}

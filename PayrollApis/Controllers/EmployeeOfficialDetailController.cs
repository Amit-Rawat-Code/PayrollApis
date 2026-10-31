using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PayrollApis.Models;
using PayrollApis.ModelServices;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PayrollApis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeOfficialDetailController : BaseController
    {
        private readonly EmployeeOffDetailServices employeeOffDetailServices;
        private readonly SqlDbContext sqlDbContext;

        public EmployeeOfficialDetailController(EmployeeOffDetailServices employeeOffDetailServices,SqlDbContext sqlDbContext)
        {
            this.employeeOffDetailServices = employeeOffDetailServices;
            this.sqlDbContext = sqlDbContext;
        }
        // GET: api/<EmployeeOfficialDetailController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var empOffList = await employeeOffDetailServices.GetEmployeeOfficialDetailList();
            return ApiSuccess(empOffList, "Employee Official detail list");
        }

        // GET api/<EmployeeOfficialDetailController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var empOffDetail = await employeeOffDetailServices.GetEmployeeOfficialDetailById(id);
            if (empOffDetail == null)
                return ApiError("This employee detail not found");
            string empName = await sqlDbContext.Employees.Where(e => e.EmployeeId == empOffDetail.EmployeeId).Select(e=>e.EmployeeName).FirstOrDefaultAsync();
            return ApiSuccess(empOffDetail, "Official Detail of" + empName);
        }

        // POST api/<EmployeeOfficialDetailController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] EmployeeOfficialDetail value)
        {
            var empOffDetail = await employeeOffDetailServices.AddEmployeeOfficialDetail(value);
            if (empOffDetail == null)
                return ApiError("Already exists");
            return ApiSuccess(empOffDetail, "Added successfully");
        }

        // PUT api/<EmployeeOfficialDetailController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] EmployeeOfficialDetail value)
        {
            var empOffDetail =await employeeOffDetailServices.UpdateEmployeeOfficialDetail(id, value);
            if (empOffDetail == null)
                return ApiError("Record not found");
            return ApiSuccess(empOffDetail, "Updated successfully");
        }

        // DELETE api/<EmployeeOfficialDetailController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var Isdeleted = await employeeOffDetailServices.DeleteEmployeeOfficialDetail(id);
            if (!Isdeleted)
                return ApiError("Record not found");
            return ApiSuccess(Isdeleted, "Deleted successfully");
        }
        [HttpGet("emp-list-of-manager")]
        public async Task<IActionResult> GetEmployeeListForManager()
        {
            var result = await employeeOffDetailServices.GetReportingHierarchy();
            return ApiSuccess(result, "Employee List For Manager");
        }
    }
}

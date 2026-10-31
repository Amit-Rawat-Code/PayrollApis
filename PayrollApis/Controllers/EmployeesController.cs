using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PayrollApis.DtosModels;
using PayrollApis.Models;
using PayrollApis.ModelServices;

namespace PayrollApis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly EmployeeServices employeeServices;

        public EmployeesController(EmployeeServices employeeServices)
        {
            this.employeeServices = employeeServices;
        }

        // GET: api/Employees
        [HttpGet]
        public async Task<IActionResult> GetEmployees()
        {
            var employees = await employeeServices.GetAllEmployees();
            return Ok(employees);
        }

        // GET: api/Employees/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployee(int id)
        {
            var employee = await employeeServices.GetEmployeeById(id);

            return Ok(employee);
        }

        // PUT: api/Employees/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee(int id, Employee employee)
        {
            if (id != employee.EmployeeId)
            {
                return BadRequest("Employee ID mismatch");
            }

            var emp = await employeeServices.UpdateEmployee(employee);
            if (employee == null)
            {
                return NotFound("Employee not found");
            }
            return Ok(emp);
        }

        // POST: api/Employees
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult> PostEmployee([FromForm] EmployeeDto dto)
        {
            string? ImagePath = null;
            try
            {
                if (dto.Image != null)
                {
                    var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
                    if (!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(dto.Image.FileName);
                    var filePath= Path.Combine(folderPath, fileName);
                    using(var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await dto.Image.CopyToAsync(stream);
                    }
                    ImagePath = filePath;
                }
                var employee = new Employee
                {
                    EmployeeName = dto.EmployeeName,
                    EmpCode = dto.EmpCode,
                    Mobile = dto.Mobile,
                    DOB = dto.DOB,
                    IsManager = dto.IsManager,
                    Address = dto.Address,
                    Gender = dto.Gender,
                    ImagePath = ImagePath,
                    IsActive = true
                };
                var emp = await employeeServices.AddEmployee(employee);
                return CreatedAtAction("GetEmployee", new { id = employee.EmployeeId }, employee);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }            
        }

        // DELETE: api/Employees/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var employee = await employeeServices.DeleteEmployee(id);
            if (!employee)
            {
                return NotFound("Employee not found");
            }

            return Ok(employee);
        }

        private async Task<IActionResult> EmployeeExists(int id)
        {
            var emp = await employeeServices.IsEmployeeExists(id);
            return emp ? Ok(emp) : NotFound("Employee not found");
        }
    }
}

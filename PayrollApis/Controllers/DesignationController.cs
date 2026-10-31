using Microsoft.AspNetCore.Mvc;
using PayrollApis.Models;
using PayrollApis.ModelServices;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PayrollApis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DesignationController : BaseController
    {
        private readonly DesignationServices designationServices;

        public DesignationController(DesignationServices designationServices)
        {
            this.designationServices = designationServices;
        }
        // GET: api/<DesignationController>
        [HttpGet]
        public async Task<IActionResult> GetDesignations()
        {
            var desn = await designationServices.GetAllDesignations();
            return ApiSuccess(desn, "Designation list");
        }

        // GET api/<DesignationController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDesignationById(int id)
        {
            var desn = await designationServices.GetDesignationById(id);
            if (desn == null) 
            {
                return ApiError("Record Not found");
            }
            return ApiSuccess(desn, desn.DesignationName);
        }

        // POST api/<DesignationController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Designation desn)
        {
            var _desn = await designationServices.AddDesigantion(desn);
            if(desn == null)
            {
                return ApiError("Failed to add Designation");
            }
            return ApiSuccess(desn, "Desigantion added");
        }

        // PUT api/<DesignationController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Designation desn)
        {
            if (id != desn.DesignationId) return ApiError("DesigantionId mismatched");

            var _desn = await designationServices.UpdateDesigantion(desn);
            if (desn is null) return ApiError("Designation not found");

            return ApiSuccess(desn, "Desigantion updated");
        }

        // DELETE api/<DesignationController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var isDeleted=await designationServices.DeleteDesignation(id);
            if (!isDeleted)
                return ApiError("Designation not found");
            return ApiSuccess(isDeleted, "Desigantion deleted");
        }
    }
}

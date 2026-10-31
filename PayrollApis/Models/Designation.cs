using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PayrollApis.Models
{
    public class Designation : CommonModel
    {
        [Key]
        public int DesignationId { get; set; }
        public int DepartmentId { get; set; }
        [Column(TypeName ="nvarchar(50)")]
        public string? DesignationName { get; set; }
        [Column(TypeName = "nvarchar(250)")]
        public string? Description { get; set; }
    }
}

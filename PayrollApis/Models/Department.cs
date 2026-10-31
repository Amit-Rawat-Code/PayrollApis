using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PayrollApis.Models
{
    public class Department : CommonModel
    {
        [Key]
        public int DepartmentId { get; set; }
        [Column(TypeName ="nvarchar(100)")]
        public string DepartmentName { get; set; } = string.Empty;
        [Column(TypeName ="nvarchar(250)")]
        public string Description { get; set; } = string.Empty;
    }
}

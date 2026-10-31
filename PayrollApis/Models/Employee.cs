using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PayrollApis.Models
{
    public class Employee : CommonModel
    {
        [Key]
        public int EmployeeId { get; set; }
        [Column(TypeName = "nvarchar(20)")]
        public string EmpCode { get; set; } = null!;
        public string EmployeeName { get; set; } = string.Empty;
        public DateTime DOB { get; set; }
        public string Gender { get; set; } = string.Empty;
        [Column(TypeName = "nvarchar(20)")]
        public string Mobile { get; set; } = string.Empty;        
        public bool IsManager { get; set; }
        [Column(TypeName = "nvarchar(300)")]
        public string Address { get; set; } = string.Empty;
        public string? ImagePath { get; set; }
    }
}

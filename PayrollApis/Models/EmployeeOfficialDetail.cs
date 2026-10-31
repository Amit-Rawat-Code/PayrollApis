using System.ComponentModel.DataAnnotations;
using static System.Net.Mime.MediaTypeNames;

namespace PayrollApis.Models
{
    public class EmployeeOfficialDetail : CommonModel
    {
        [Key]
        public int EmpOfficeId { get; set; }
		public int EmployeeId { get; set; }
        public int DepartmentId { get; set; }
        public int DesignationId { get; set; }
        public DateTime DOJ { get; set; }
        public string EmploymentType { get; set; } = string.Empty;
		public int ReportingManagerId { get; set; }
    }
}

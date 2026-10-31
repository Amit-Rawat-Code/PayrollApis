namespace PayrollApis.DtosModels
{
    public class EmployeeDto : Common
    {
        public int EmployeeId { get; set; }
        public string EmpCode { get; set; } = string.Empty;
        public string EmployeeName { get; set; } = string.Empty;
        public DateTime DOB { get; set; }
        public string Mobile { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public bool IsManager { get; set; }
        public int EmpOfficeId { get; set; }
        public int DepartmentId { get; set; }
        public int DesignationId { get; set; }
        public string DepartmentName { get; set; } = string.Empty;
        public string DesignationName { get; set; } = string.Empty;
        public DateTime DOJ { get; set; }
        public string EmploymentType { get; set; } = string.Empty;
        public int ReportingManagerId { get; set; }
        public IFormFile? Image { get; set; }
    }
}

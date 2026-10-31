namespace PayrollApis.DtosModels
{
    public class EmployeeSalaryTransactionDto : Common
    {
        public int SalaryTransactionId { get; set; }
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; } = string.Empty;
        public int Month { get; set; }
        public int Year { get; set; }
        public int PresentDays { get; set; }
        public float TotalHours { get; set; }
        public float PF { get; set; }
        public float ESI { get; set; }
        public float ProfessionalTax { get; set; }
        public float GrossSalary { get; set; }
        public float TotalDeduction { get; set; }
        public float NetSalary { get; set; }
        public DateTime? ProcessedOn { get; set; }
        public int SalaryType { get; set; }
    }
}

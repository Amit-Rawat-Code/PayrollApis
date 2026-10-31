using System.ComponentModel.DataAnnotations;

namespace PayrollApis.Models
{
    public class EmployeeSalaryStructure : CommonModel
    {
        [Key]
        public int SalaryStructureId { get; set; }
        public int EmployeeId { get; set; }
        public float Basic { get; set; }
        public float HRA { get; set; }
        public float DA { get; set; }
        public float ConveyanceAllowance { get; set; }
        public float MedicalAllowance { get; set; }
        public float SpecialAllowance { get; set; }
        public int SalaryType { get; set; }
        public float DailyRate { get; set; }
        public float HourlyRate { get; set; }
        public int WorkingDaysPerMonth { get; set; }
        public DateTime EffectiveFrom { get; set; }
    }
}

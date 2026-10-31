using Humanizer;
using System.ComponentModel.DataAnnotations;

namespace PayrollApis.Models
{
    public class EmployeeStatuary : CommonModel
    {
        [Key]
        public int StatutoryId { get; set; }
        public int EmployeeId	{ get; set; }
        public bool IsPfApplicable { get; set; }
        public string UAN { get; set; } = string.Empty;
        public string PFAccountNo { get; set; } = string.Empty;
        public bool IsEsiApplicable { get; set; }
        public string EsiNo { get; set; } = string.Empty;
        public DateTime EffectiveFrom { get; set; }
    }
}

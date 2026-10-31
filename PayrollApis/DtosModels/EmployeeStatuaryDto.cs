namespace PayrollApis.DtosModels
{
    public class EmployeeStatuaryDto : Common
    {
        public int StatutoryId { get; set; }
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; } = string.Empty;
        public bool IsPfApplicable { get; set; }
        public string UAN { get; set; } = string.Empty;
        public string PFAccountNo { get; set; } = string.Empty;
        public bool IsEsiApplicable { get; set; }
        public string EsiNo { get; set; } = string.Empty;
        public DateTime EffectiveFrom { get; set; }
    }
}

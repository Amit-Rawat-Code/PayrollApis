namespace PayrollApis.DtosModels
{
    public class EmployeeBankDetailDto : Common
    {
        public int BankId { get; set; }
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; } = string.Empty;
        public string BankName { get; set; } = string.Empty;
        public string AccountNo { get; set; } = string.Empty;
        public string IFSC { get; set; } = string.Empty;
        public string PAN { get; set; } = string.Empty;
        public string Aadhaar { get; set; } = string.Empty;
        public string PaymentMode { get; set; } = string.Empty;
    }
}

using PayrollApis.DtosModels;
using PayrollApis.ModelServices;

namespace PayrollApis.Helper
{
    public enum SalaryType
    {
        Monthly = 1,
        Daily = 2,
        Hourly = 3
    }
    public static class PayrollCalculator
    {
        private const float PFWageCeiling = 15000;
        private const float PFPercent = 12;
        private const float ESILimit = 21000;
        private const float ESIPercent = 0.75f;
        private const float ProfessionalTax = 200;

        public static EmployeeSalaryTransactionDto Calculate(EmployeeSalaryStructureDto salary, EmployeeStatuaryDto statutory, int presentDays, float totalHours, int month, int year)
        {
            float gross = salary.SalaryType switch
            {
                (int)SalaryType.Monthly =>
                    ((salary.Basic +
                      salary.DA+
                      salary.HRA +
                      salary.ConveyanceAllowance +
                      salary.MedicalAllowance +
                      salary.SpecialAllowance)
                     / salary.WorkingDaysPerMonth) * presentDays,

                (int)SalaryType.Daily =>
                    salary.DailyRate * presentDays,

                (int)SalaryType.Hourly =>
                    salary.HourlyRate *  totalHours,

                _ => 0
            };

            float pf = 0;
            if (statutory.IsPfApplicable)
            {
                float pfBase = Math.Min(salary.Basic, PFWageCeiling);
                pf = pfBase * PFPercent / 100;
            }

            float esi = 0;
            if (statutory.IsEsiApplicable && gross <= ESILimit)
            {
                esi = gross * ESIPercent / 100;
            }

            float deductions = pf + esi + ProfessionalTax;

            return new EmployeeSalaryTransactionDto
            {
                EmployeeId = salary.EmployeeId,
                Month = month,
                Year = year,
                PresentDays = presentDays,
                TotalHours = totalHours,

                GrossSalary = (float)Math.Round(gross, 2),
                PF = (float)Math.Round(pf, 2),
                ESI = (float)Math.Round(esi, 2),
                ProfessionalTax = ProfessionalTax,
                TotalDeduction = (float)Math.Round(deductions, 2),
                NetSalary = (float)Math.Round(gross - deductions, 2)
            };
        }
    }
}

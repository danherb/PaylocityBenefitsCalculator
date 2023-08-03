namespace Api.Services.BenefitCosts
{
    using Api.Dtos.Employee;

    public interface IBenefitCostsService
    {
        decimal GetMonthlyPaycheck(GetEmployeeDto employee);
        decimal GetMonthlyPaycheckBeforeDeductions(GetEmployeeDto employee);
        decimal GetBaseCost();
        decimal GetDependentsCosts(GetEmployeeDto employee);
        decimal GetHighSalaryAdditionalCost(GetEmployeeDto employee);
        decimal GetHighAgeAdditionalCost(GetEmployeeDto employee);
    }
}

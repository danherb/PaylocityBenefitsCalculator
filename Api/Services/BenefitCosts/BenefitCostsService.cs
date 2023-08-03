namespace Api.Services.BenefitCosts
{
    using Api.Dtos.Employee;

    public class BenefitCostsService : IBenefitCostsService
    {
        // Normally, it would be probably fetched from DB
        const int PaychecksPerYear = 26;
        const int BaseCost = 1000;
        const int PerDependentCost = 600;
        const int HighSalaryAdditionalCostLimit = 80000;
        const decimal HighSalaryAdditionalCost = 0.02M;
        const int HighAgeLimitInYears = 50;
        const int HighAgeAdditionalCost = 200;

        // Here, just employee id would be better, this is just not correct. 
        public decimal GetMonthlyPaycheck(GetEmployeeDto employee)
        {
            var result = GetMonthlyPaycheckBeforeDeductions(employee)
                - GetBaseCost()
                - GetDependentsCosts(employee)
                - GetHighSalaryAdditionalCost(employee)
                - GetHighAgeAdditionalCost(employee);

            return result;
        }

        // These methods may seem too simple to be standalone methods, maybe it could be just one method. But if they were more complicated,
        // probably it'd be better to keep them like this because of testing, so we can test smaller steps (although I wont create tests for them)
        public decimal GetMonthlyPaycheckBeforeDeductions(GetEmployeeDto employee)
            => employee.Salary / PaychecksPerYear;

        public decimal GetBaseCost()
            => BaseCost;

        public decimal GetDependentsCosts(GetEmployeeDto employee)
            => employee.Dependents.Count * PerDependentCost;

        public decimal GetHighSalaryAdditionalCost(GetEmployeeDto employee)
            => employee.Salary >= HighSalaryAdditionalCostLimit ? GetMonthlyPaycheckBeforeDeductions(employee) * HighSalaryAdditionalCost : 0;

        public decimal GetHighAgeAdditionalCost(GetEmployeeDto employee)
            => employee.Age >= HighAgeLimitInYears ? HighAgeAdditionalCost : 0;
    }
}

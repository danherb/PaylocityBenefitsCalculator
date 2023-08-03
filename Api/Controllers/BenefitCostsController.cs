using Api.Dtos.BenefitCosts;
using Api.Models;
using Api.Services.BenefitCosts;
using Api.Services.Employee;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class BenefitCostsController : ControllerBase
{
    private readonly IEmployeeService _employeeService;
    private readonly IBenefitCostsService _benefitCostsService;

    public BenefitCostsController(IEmployeeService employeeService, IBenefitCostsService benefitCostsService)
    {
        _employeeService = employeeService;
        _benefitCostsService = benefitCostsService;
    }

    [SwaggerOperation(Summary = "Get employee's paycheck by employee's ID")]
    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<GetBenefitCostsDto>>> GetPaycheckByEmployeeId(int id)
    {
        var employee = await _employeeService.GetEmployeeByIdAsync(id);

        if (employee == null)
            return NotFound();

        var benefitCosts = _benefitCostsService.GetMonthlyPaycheck(employee);

        return new ApiResponse<GetBenefitCostsDto> { Success = true, Data = new GetBenefitCostsDto { 
            EmployeeId = employee.Id,
            Paycheck = benefitCosts
        } };
    }
}

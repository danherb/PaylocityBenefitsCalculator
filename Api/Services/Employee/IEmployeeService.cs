using Api.Dtos.Employee;

namespace Api.Services.Employee
{
    public interface IEmployeeService
    {
        //public ApiResponse<GetEmployeeDto> AddEmployee();
        //public void UpdateEmployee();
        //public void DeleteEmployee(int id);
        public Task<IEnumerable<GetEmployeeDto>> GetAllEmployeesAsync();
        public Task<GetEmployeeDto> GetEmployeeByIdAsync(int id);
    }
}

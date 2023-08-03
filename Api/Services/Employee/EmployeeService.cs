using Api.Db;
using Api.Dtos.Employee;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace Api.Services.Employee
{
    public class EmployeeService : IEmployeeService
    {
        private readonly CustomDbContext _dbContext;
        private readonly IMapper _mapper;

        public EmployeeService(CustomDbContext dbContext, IMapper mapper)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<GetEmployeeDto>> GetAllEmployeesAsync()
        {
            var employees = _dbContext.Employees.Include(x => x.Dependents);

            return await employees.Select(x => _mapper.Map<GetEmployeeDto>(x)).ToListAsync();
        }

        public async Task<GetEmployeeDto> GetEmployeeByIdAsync(int id)
        {
            var employee = await _dbContext.Employees.Include(x => x.Dependents).FirstOrDefaultAsync(x => x.Id == id);

            if (employee == null)
                return null;

            return _mapper.Map<GetEmployeeDto>(employee);
        }
    }
}

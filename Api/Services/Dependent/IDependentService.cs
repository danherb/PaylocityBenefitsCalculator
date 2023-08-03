using Api.Dtos.Dependent;
using Api.Dtos.Employee;

namespace Api.Services.Dependent
{
    public interface IDependentService
    {
        public Task<IEnumerable<GetDependentDto>> GetAllDependentsAsync();
        public Task<GetDependentDto> GetDependentByIdAsync(int id);
    }
}

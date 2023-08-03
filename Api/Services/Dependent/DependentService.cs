using Api.Db;
using Api.Dtos.Dependent;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Api.Services.Dependent
{
    public class DependentService : IDependentService
    {
        private readonly CustomDbContext _dbContext;
        private readonly IMapper _mapper;

        public DependentService(CustomDbContext dbContext, IMapper mapper)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<GetDependentDto>> GetAllDependentsAsync()
        {
            var dependents = _dbContext.Dependents.Include(x => x.Employee);
            var list = await dependents.Select(x => _mapper.Map<GetDependentDto>(x)).ToListAsync();

            return list;
        }

        public async Task<GetDependentDto> GetDependentByIdAsync(int id)
        {
            var dependent = await _dbContext.Dependents.Include(x => x.Employee).FirstOrDefaultAsync(x => x.Id == id);

            if (dependent == null)
                return null;

            return _mapper.Map<GetDependentDto>(dependent);
        }
    }
}

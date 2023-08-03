using Api.Dtos.Dependent;
using Api.Dtos.Employee;
using Api.Models;
using AutoMapper;

namespace Api
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
			CreateMap<Employee, GetEmployeeDto>()
			.ForMember(dest => dest.Dependents, opt => opt.MapFrom(src => src.Dependents));
			CreateMap<GetEmployeeDto, Employee>()
				 .ForMember(dest => dest.Dependents, opt => opt.MapFrom(src => src.Dependents));
			CreateMap<Dependent, GetDependentDto>();
			CreateMap<GetDependentDto, Dependent>();
		}
    }
}

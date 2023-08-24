using AutoMapper;
using EFCoreApi.Models;
using EFCoreApi.Models.Dto;

namespace EFCoreApi.Data.Configuration
{
    public class MappingConfig : Profile
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<EmployeeDto, Employees>();
                config.CreateMap<Employees, EmployeeDto>();

            });

            return mappingConfig;
        }
    }
}

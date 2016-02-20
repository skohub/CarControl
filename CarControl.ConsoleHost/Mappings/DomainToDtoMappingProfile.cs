using System.Linq;
using AutoMapper;
using CarConnect.Model;
using CarControl.Contract;

namespace CarControl.ConsoleHost.Mappings
{
    public class DomainToDtoMappingProfile : Profile
    {
        public DomainToDtoMappingProfile() : base("DomainToDtoMappings") { }

        protected override void Configure()
        {
            CreateMap<Car, CarDto>();
        }
    }
}
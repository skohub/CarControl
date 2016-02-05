using AutoMapper;
using CarConnect.Model;
using CarControl.CarConnect.Protocol;
using CarControl.Web.Models;

namespace CarControl.Web.Mappings
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile() : base("DomainToViewModelMappings") { }

        protected override void Configure()
        {
            Mapper.CreateMap<Car, CarViewModel>();
            Mapper.CreateMap<ICarProtocol, CarViewModel>();
        }
    }
}
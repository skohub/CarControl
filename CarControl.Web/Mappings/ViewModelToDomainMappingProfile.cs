using AutoMapper;
using CarControl.Web.Models;

namespace CarControl.Web.Mappings
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile() : base("ViewModelToDomainMappings") { }

        protected override void Configure()
        {
            //Mapper.CreateMap<CarViewModel, Car>();
        }
    }
}
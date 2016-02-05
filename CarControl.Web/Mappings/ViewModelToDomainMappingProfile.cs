using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using CarConnect.Model;
using CarControl.Web.Models;

namespace CarControl.Web.Mappings
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile() : base("ViewModelToDomainMappings") { }

        protected override void Configure()
        {
            Mapper.CreateMap<CarViewModel, Car>();
        }
    }
}
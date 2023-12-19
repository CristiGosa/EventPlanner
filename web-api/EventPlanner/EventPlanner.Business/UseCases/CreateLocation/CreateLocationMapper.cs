using AutoMapper;
using EventPlanner.Domain.Entities;

namespace EventPlanner.Business.UseCases.CreateLocation
{
    public sealed class CreateLocationMapper : Profile
    {
        public CreateLocationMapper()
        {
            CreateMap<CreateLocationRequest, Location>();
        }
    }
}

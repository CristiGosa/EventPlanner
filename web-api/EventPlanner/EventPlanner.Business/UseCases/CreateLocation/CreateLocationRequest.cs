using EventPlanner.Domain.Entities;
using MediatR;

namespace EventPlanner.Business.UseCases.CreateLocation
{
    public sealed record CreateLocationRequest : IRequest<Location>
    {
        public string Name { get; set; }
        public int Capacity { get; set; }
    }
}

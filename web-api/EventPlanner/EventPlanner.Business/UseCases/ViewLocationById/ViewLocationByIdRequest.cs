using MediatR;

namespace EventPlanner.Business.UseCases.ViewLocationById
{
    public sealed class ViewLocationByIdRequest : IRequest<ViewLocationByIdResponse>
    {
        public int Id { get; set; }
    }
}

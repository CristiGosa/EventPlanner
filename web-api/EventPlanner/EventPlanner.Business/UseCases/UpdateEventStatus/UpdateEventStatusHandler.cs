using EventPlanner.Domain.Repositories;

using MediatR;

namespace EventPlanner.Business.UseCases.UpdateEventStatus;
public sealed class UpdateEventStatusHandler : IRequestHandler<UpdateEventStatusRequest, UpdateEventStatusResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateEventStatusHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<UpdateEventStatusResponse> Handle(UpdateEventStatusRequest request, CancellationToken cancellationToken)
    {
        await _unitOfWork.Events.UpdateStatusAsync(request.EventId, request.NewStatus);

        return new UpdateEventStatusResponse { EventId = request.EventId, NewStatus = request.NewStatus };
    }
}

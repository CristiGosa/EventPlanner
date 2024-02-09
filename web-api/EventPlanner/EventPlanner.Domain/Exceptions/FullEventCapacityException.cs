using EventPlanner.Domain.Exceptions.Interfaces;

namespace EventPlanner.Domain.Exceptions
{
    public sealed class FullEventCapacityException : Exception, ITemplatedError
    {
        private const string MessageTemplate = "The event does not allow any more reservations";

        public FullEventCapacityException() : base(MessageTemplate)
        {

        }

        public string ErrorCode => "FullEventCapacity";
        public string MessageDetails => MessageTemplate;
    }
}

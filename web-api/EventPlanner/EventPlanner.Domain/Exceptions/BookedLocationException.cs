using EventPlanner.Domain.Exceptions.Interfaces;

namespace EventPlanner.Domain.Exceptions
{
    public sealed class BookedLocationException : Exception, ITemplatedError
    {
        private const string MessageTemplate = "The location is booked in the selected timeframe";

        public BookedLocationException() : base(MessageTemplate)
        {

        }

        public string ErrorCode => "BookedLocation";
        public string MessageDetails => MessageTemplate;
    }
}

namespace TravelAgency.Domain.Exceptions
{
    // User request have errors
    public class BadRequestException : Exception
    {
        public BadRequestException(string message) : base(message) { }
    }
}

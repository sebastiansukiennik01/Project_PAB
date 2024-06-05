namespace TravelAgency.Domain.Exceptions
{
    // Object was not found in the context
    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message) { }
    }
}

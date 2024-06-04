namespace SaleKiosk.Domain.Exceptions
{
    // Object was not found
    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message) { }
    }
}

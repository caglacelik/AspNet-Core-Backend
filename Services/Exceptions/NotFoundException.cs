namespace API.Midllewares
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string exceptionMessage) : base(exceptionMessage)
        {

        }
    }
}

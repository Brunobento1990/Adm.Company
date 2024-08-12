namespace Adm.Company.Domain.Exceptions;

public class ExceptionApiUnauthorized : Exception
{
    public ExceptionApiUnauthorized(string message) : base(message)
    {
    }
}

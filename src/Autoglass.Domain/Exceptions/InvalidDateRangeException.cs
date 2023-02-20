namespace Autoglass.Domain.Exceptions
{
    public class InvalidDateRangeException : Exception
    {
        public InvalidDateRangeException() : base("Intervalo de datas inválido.")
        {
        }

        public InvalidDateRangeException(string message) : base(message)
        {
        }

        public InvalidDateRangeException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
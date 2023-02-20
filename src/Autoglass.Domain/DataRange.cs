using Autoglass.Domain.Exceptions;

namespace Autoglass.Domain
{
    public class DateRange
    {
        public DateTime ManufacturingDate { get; }
        public DateTime ExpirationDate { get; }

        public DateRange(DateTime manufacturingDate, DateTime expirationDate)
        {
            if (manufacturingDate >= expirationDate)
                throw new InvalidDateRangeException("A data de fabricação não pode ser maior ou igual à data de vencimento.");

            if (expirationDate < DateTime.Today)
                throw new InvalidDateRangeException("A data de vencimento não pode estar no passado.");

            ManufacturingDate = manufacturingDate;
            ExpirationDate = expirationDate;
        }
    }
}

using CSharpFunctionalExtensions;


namespace Catalog.Domain.Models
{
    public class PublishYear : ValueObject
    {
        public const int MIN_YEAR = 1800;

        public int Year { get; }


        private PublishYear(int year)
        {
            Year = year;
        }

        public static Result<PublishYear> Create(int year, int currentYear)
        {
            if (year < MIN_YEAR)
                return Result.Failure<PublishYear>($"'{nameof(Year)}' must be >= {MIN_YEAR}.");
            if (year > currentYear)
                return Result.Failure<PublishYear>($"'{nameof(Year)}' cannot be in the future.");

            return new PublishYear(year);
        }


        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Year;
        }
    }
}

using CSharpFunctionalExtensions;

namespace Catalog.Domain.Models
{
    public sealed class ReviewRating : ValueObject
    {
        public const int MAX_RATING = 5;
        public const int MIN_RATING = 1;

        public int Value { get; }

        private ReviewRating(int value)
        {
            Value = value;
        }

        public static Result<ReviewRating> Create(int value)
        {
            if (value is < MIN_RATING or > MAX_RATING)
                return Result.Failure<ReviewRating>($"Rating must be between {MIN_RATING} and {MAX_RATING}.");

            return new ReviewRating(value);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}

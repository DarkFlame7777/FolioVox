using CSharpFunctionalExtensions;

namespace Catalog.Domain.Models
{
    public sealed class Isbn : ValueObject
    {
        public const int ISBN_13_LENGTH = 13;
        public const int ISBN_10_LENGTH = 10;

        public string Value { get; }

        private Isbn(string value)
        {
            Value = value;
        }

        public static Result<Isbn> Create(string? input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return Result.Failure<Isbn>("ISBN cannot be empty.");

            var clean = input.Replace("-", "")
                             .Replace(" ", "")
                             .Trim()
                             .ToUpperInvariant();

            if (clean.Length == ISBN_10_LENGTH)
            {
                if (!IsValidIsbn10(clean))
                    return Result.Failure<Isbn>("Invalid ISBN-10 format or checksum.");

                var isbn13 = ConvertIsbn10ToIsbn13(clean);
                return Result.Success(new Isbn(isbn13));
            }

            if (clean.Length == ISBN_13_LENGTH)
            {
                if (!clean.StartsWith("978") && !clean.StartsWith("979"))
                    return Result.Failure<Isbn>("ISBN-13 must start with 978 or 979.");

                if (!IsValidIsbn13(clean))
                    return Result.Failure<Isbn>("Invalid ISBN-13 checksum.");

                return Result.Success(new Isbn(clean));
            }

            return Result.Failure<Isbn>($"ISBN must be {ISBN_10_LENGTH} or {ISBN_13_LENGTH} digits long.");
        }


        private static bool IsValidIsbn10(string isbn10)
        {
            int sum = 0;
            for (int i = 0; i < 9; i++)
            {
                if (!char.IsDigit(isbn10[i])) return false;
                sum += (isbn10[i] - '0') * (10 - i);
            }

            char last = isbn10[9];
            if (last == 'X') sum += 10;
            else if (char.IsDigit(last)) sum += (last - '0');
            else return false;

            return sum % 11 == 0;
        }

        private static bool IsValidIsbn13(string isbn13)
        {
            if (!isbn13.All(char.IsDigit)) return false;

            int sum = 0;
            for (int i = 0; i < 12; i++)
            {
                int digit = isbn13[i] - '0';
                sum += (i % 2 == 0) ? digit : digit * 3;
            }

            int checkDigit = isbn13[12] - '0';
            int calculatedCheckDigit = (10 - (sum % 10)) % 10;

            return checkDigit == calculatedCheckDigit;
        }

        private static string ConvertIsbn10ToIsbn13(string isbn10)
        {
            string core = "978" + isbn10.Substring(0, 9);

            int sum = 0;
            for (int i = 0; i < 12; i++)
            {
                int digit = core[i] - '0';
                sum += (i % 2 == 0) ? digit : digit * 3;
            }

            int checkDigit = (10 - (sum % 10)) % 10;

            return core + checkDigit;
        }


        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
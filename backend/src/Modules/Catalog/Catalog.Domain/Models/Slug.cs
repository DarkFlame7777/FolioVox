using CSharpFunctionalExtensions;
using System.Text.RegularExpressions;

namespace Catalog.Domain.Models
{
    public sealed partial class Slug : ValueObject
    {
        public const int MAX_LENGTH = 70;

        public string Value { get; }

        private Slug(string value)
        {
            Value = value;
        }

        public static Result<Slug> Create(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return Result.Failure<Slug>("Slug cannot be empty.");

            var slug = input.ToLowerInvariant().Trim();

            slug = slug.Replace(" ", "-");

            slug = InvalidCharsRegex().Replace(slug, "");

            slug = MultipleDashesRegex().Replace(slug, "-");

            slug = slug.Trim('-');

            if (string.IsNullOrWhiteSpace(slug))
                return Result.Failure<Slug>("Slug is invalid after sanitization.");

            if (input.Length > MAX_LENGTH)
                return Result.Failure<Slug>("Slug can not be longer then 70.");
                
            return new Slug(slug);
        }

        [GeneratedRegex(@"[^a-z0-9\-]", RegexOptions.Compiled)]
        private static partial Regex InvalidCharsRegex();

        [GeneratedRegex(@"\-{2,}", RegexOptions.Compiled)]
        private static partial Regex MultipleDashesRegex();


        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}

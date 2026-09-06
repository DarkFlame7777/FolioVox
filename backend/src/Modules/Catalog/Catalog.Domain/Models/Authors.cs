using CSharpFunctionalExtensions;

namespace Catalog.Domain.Models
{
    public sealed class Authors : ValueObject
    {
        public IReadOnlyList<string> Items { get; }

        private Authors(List<string> items) => Items = items.AsReadOnly();

        public static Result<Authors> Create(IEnumerable<string>? authors)
        {
            if (authors == null)
                return Result.Failure<Authors>("Authors list cannot be null.");

            var cleaned = authors
                .Where(a => !string.IsNullOrWhiteSpace(a))
                .Select(a => a.Trim())
                .ToList();

            if (cleaned.Count == 0)
                return Result.Failure<Authors>("Book must have at least one author.");

            foreach (var author in cleaned)
            {
                if (author.Length > 150)
                    return Result.Failure<Authors>($"Author name '{author}' exceeds 150 chars.");
            }

            return new Authors(cleaned);
        }

        public static Result<Authors> CreateFromSingleString(string rawAuthors)
        {
            if (string.IsNullOrWhiteSpace(rawAuthors))
                return Result.Failure<Authors>("Authors string cannot be empty.");

            var split = rawAuthors.Split(new[] { ',', ';' }, StringSplitOptions.RemoveEmptyEntries);
            return Create(split);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            foreach (var author in Items) yield return author;
        }
    }
}

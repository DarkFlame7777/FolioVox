using CSharpFunctionalExtensions;

namespace Catalog.Domain.Models
{
    public sealed class BookMetadata : ValueObject
    {
        public const int MAX_TITLE_LENGTH = 250;
        public const int MAX_DESCRIPTION_LENGTH = 2000;
        public const int MAX_PUBLISHER_LENGTH = 100;
        public const int MAX_AUTHOR_LENGTH = 150;
        public const int MAX_PAGES = 10000;

        public string Title { get; }
        public string? Description { get; }
        public string Publisher { get; }
        public PublishYear Year { get; }
        public string Author { get; }
        public int Pages { get; }

        private BookMetadata(
            string title,
            string? description,
            string publisher,
            PublishYear year,
            string author,
            int pages)
        {
            Title = title;
            Description = description;
            Publisher = publisher;
            Year = year;
            Author = author;
            Pages = pages;
        }


        public static Result<BookMetadata> Create(
            string title,
            string? description,
            string publisher,
            PublishYear year,
            string author,
            int pages)
        {
            if (string.IsNullOrWhiteSpace(title))
                return Result.Failure<BookMetadata>($"'{nameof(Title)}' cannot be empty.");
            if (title.Length > MAX_TITLE_LENGTH)
                return Result.Failure<BookMetadata>($"'{nameof(Title)}' max {MAX_TITLE_LENGTH} chars.");

            if (description?.Length > MAX_DESCRIPTION_LENGTH)
                return Result.Failure<BookMetadata>($"'{nameof(Description)}' max {MAX_DESCRIPTION_LENGTH} chars.");

            if (string.IsNullOrWhiteSpace(publisher))
                return Result.Failure<BookMetadata>($"'{nameof(Publisher)}' cannot be empty.");
            if (publisher.Length > MAX_PUBLISHER_LENGTH)
                return Result.Failure<BookMetadata>($"'{nameof(Publisher)}' max {MAX_PUBLISHER_LENGTH} chars.");

            if (string.IsNullOrWhiteSpace(author))
                return Result.Failure<BookMetadata>($"'{nameof(Author)}' cannot be empty.");
            if (author.Length > MAX_AUTHOR_LENGTH)
                return Result.Failure<BookMetadata>($"'{nameof(Author)}' max {MAX_AUTHOR_LENGTH} chars.");

            if (pages <= 0)
                return Result.Failure<BookMetadata>($"'{nameof(Pages)}' must be greater than 0.");
            if (pages > MAX_PAGES)
                return Result.Failure<BookMetadata>($"'{nameof(Pages)}' max {MAX_PAGES} pages.");


            

            return new BookMetadata(
                title.Trim(),
                description?.Trim(),
                publisher.Trim(),
                year,
                author.Trim(),
                pages); 
        }


        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Title;
            if (Description != null) yield return Description;
            yield return Publisher;
            yield return Year;
            yield return Author;
            yield return Pages;
        }
    }
}

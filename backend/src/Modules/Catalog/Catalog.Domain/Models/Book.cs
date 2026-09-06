using CSharpFunctionalExtensions;

namespace Catalog.Domain.Models
{
    public sealed class Book : Entity<BookId>
    {
        private int _totalRatingSum;
        private int _ratingCount;

        public Isbn Isbn { get; }
        public CoverImage? Image { get; }
        public BookMetadata BookInfo { get; }
        public IReadOnlyList<GenreId> Genres { get; }

        


        public double? Rating => _ratingCount == 0 ? null : (double) _totalRatingSum / _ratingCount;


        private Book(
             BookId id, 
             Isbn isbn, 
             BookMetadata bookMetadata,
             IReadOnlyList<GenreId> genres, 
             CoverImage? image) : base(id)
        {
            Isbn = isbn;
            BookInfo = bookMetadata;
            Genres = genres;
            Image = image;
        }


        public static Result<Book> Create(
            BookId id,
            Isbn isbn, 
            BookMetadata bookMetadata,
            IReadOnlyList<GenreId> genres,
            CoverImage? coverImage)
        {
            return new Book(
                id,
                isbn,
                bookMetadata,
                genres,
                coverImage);
        }

        public void UpdateRating(ReviewRating newRating)
        {
            _totalRatingSum += newRating.Value;
            _ratingCount++;
        }
    }
}
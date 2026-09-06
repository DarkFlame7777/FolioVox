using CSharpFunctionalExtensions;

namespace Catalog.Domain.Models
{
    public sealed class Review : Entity<ReviewId>
    {
        private const int MaxCommentLength = 1000;

        public UserId UserId { get; }
        public ReviewRating Rating { get; }
        public string? Comment { get; }
        public DateTime CreatedAt { get; }

        private Review(ReviewId id, UserId userId, ReviewRating rating, string? comment, DateTime createdAt)
            : base(id)
        {
            UserId = userId;
            Rating = rating;
            Comment = comment;
            CreatedAt = createdAt;
        }

        public static Result<Review> Create(ReviewId reviewId, UserId userId, ReviewRating rating, string? comment, DateTime createdAt)
        {
            if (comment is { Length: > MaxCommentLength })
                return Result.Failure<Review>($"Comment cannot exceed {MaxCommentLength} characters.");

            return new Review(reviewId, userId, rating, comment?.Trim(), createdAt);
        }
    }
}

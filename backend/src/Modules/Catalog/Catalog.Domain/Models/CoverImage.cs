using CSharpFunctionalExtensions;

namespace Catalog.Domain.Models
{
    public sealed class CoverImage : ValueObject
    {
        public string FileName { get; }

        private CoverImage(string fileName)
        {
            FileName = fileName;
        }

        public static Result<CoverImage> Create(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
                return Result.Failure<CoverImage>("Cover file name cannot be empty.");

            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".webp", ".gif" };
            var extension = Path.GetExtension(fileName).ToLowerInvariant();
            if (!allowedExtensions.Contains(extension))
                return Result.Failure<CoverImage>("Unsupported image format. Use jpg, png, webp, gif.");

            // Защита от path traversal
            var safeName = fileName.Replace('\\', '/').Trim('/');
            if (safeName.Contains(".."))
                return Result.Failure<CoverImage>("Invalid file path.");

            return Result.Success(new CoverImage(safeName));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return FileName;
        }
    }
}
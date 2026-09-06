using CSharpFunctionalExtensions;

namespace Catalog.Domain.Models
{
    public sealed class Genre : Entity<GenreId>
    {
        public const int MAX_NAME_LENGTH = 100;
        public const int MAX_DESCRIPTION_LENGTH = 2000;

        public string Name { get; private set; }
        public string? Description { get; private set; }
        public GenreId? ParentGenreId { get; private set; }
        public Slug Slug { get; private set; }
        public bool IsArchived { get; private set; }
        

        private Genre(GenreId id, string name, string? description, GenreId? parentGenreId, Slug slug) : base(id)
        {
            Name = name;
            Description = description;
            ParentGenreId = parentGenreId;
            Slug = slug;
            IsArchived = false;
        }

        public static Result<Genre> Create(GenreId genreId, string? name, Slug slug, string? description = null, GenreId? parentGenreId = null)
        {
            if (!IsValidName(name))
                return Result.Failure<Genre>($"Genre name cannot be empty and max {MAX_NAME_LENGTH} chars.");

            if (!IsValidDescription(description))
                return Result.Failure<Genre>($"'{nameof(Description)}' max {MAX_DESCRIPTION_LENGTH} chars.");

            if (parentGenreId.HasValue && parentGenreId.Value == genreId)
                return Result.Failure<Genre>("A genre cannot be its own parent.");

            return new Genre(genreId, name!.Trim(), description?.Trim(), parentGenreId, slug);
        }



        public Result ChangeMainInfo(string name, Slug slug)
        {
            if (!IsValidName(name))
                return Result.Failure($"Genre name cannot be empty and must be up to {MAX_NAME_LENGTH} characters");

            Name = name!.Trim();
            Slug = slug;

            return Result.Success();
        }

        public Result ChangeDescription(string description)
        {
            if (IsValidDescription(description))
                return Result.Failure($"Description cannot exceed {MAX_DESCRIPTION_LENGTH} characters.");    
           
            Description = description?.Trim();

            return Result.Success();
        }

        public Result ChangeParent(GenreId? newParentId)
        {
            if (newParentId == Id)
                return Result.Failure("A genre cannot be its own parent.");
            
            ParentGenreId = newParentId;
           
            return Result.Success();
        }

        public void Archive()
        {
            IsArchived = true;
        }

        public void Restore()
        {
            IsArchived = false;
        }


        private static bool IsValidName(string? name)
        {
            return !(string.IsNullOrWhiteSpace(name) || name.Length > MAX_NAME_LENGTH);
        }

        private static bool IsValidDescription(string? description)
        {
            return !(description?.Length > MAX_DESCRIPTION_LENGTH);
        }
    }
}
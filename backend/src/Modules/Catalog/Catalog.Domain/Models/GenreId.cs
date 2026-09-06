using CSharpFunctionalExtensions;
using Vogen;

namespace Catalog.Domain.Models
{
    [ValueObject<int>]
    public readonly partial struct GenreId
    {
        private static Validation Validate(int value) =>
            value > 0
                ? Validation.Ok
                : Validation.Invalid("GenreId must be greater than 0.");
    }
}

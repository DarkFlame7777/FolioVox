using Vogen;

namespace Catalog.Domain.Models
{
    [ValueObject<Guid>]
    public readonly partial struct ReviewId
    {
        private static Validation Validate(Guid value) =>
            value != Guid.Empty
                ? Validation.Ok
                : Validation.Invalid("ReviewId cannot be empty.");

    }
}

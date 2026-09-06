using Vogen;

namespace Catalog.Domain.Models
{
    [ValueObject<Guid>]
    public readonly partial struct BookId
    {
        private static Validation Validate(Guid value) =>
            value != Guid.Empty
                ? Validation.Ok
                : Validation.Invalid("BookId cannot be empty.");

    }
}

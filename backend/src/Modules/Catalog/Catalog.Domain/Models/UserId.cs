    using Vogen;

    namespace Catalog.Domain.Models
    {
        [ValueObject<Guid>]
        public readonly partial struct UserId
        {
            private static Validation Validate(Guid value) =>
                value != Guid.Empty
                    ? Validation.Ok
                    : Validation.Invalid("UserId cannot be empty.");
        }
    }

using System;
using System.Collections.Generic;
using System.Text;
using Vogen;

namespace Catalog.Domain.Models
{
    [ValueObject<Guid>]
    public readonly partial struct BookCopyId
    {
        private static Validation Validate(Guid value) =>
            value != Guid.Empty
                ? Validation.Ok
                : Validation.Invalid("BookCopyId cannot be empty.");
    }
}

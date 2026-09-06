using CSharpFunctionalExtensions;
using System.Text.RegularExpressions;

namespace Catalog.Domain.Models
{
    public sealed class InventoryNumber : ValueObject
    {
        public const int MAX_INVENTORYNUMBER_LENGTH = 50;
        public const int MIN_INVENTORYNUMBER_LENGTH = 3;

        public string Value { get; }

        private InventoryNumber(string value)
        {
            Value = value;
        }

        public static Result<InventoryNumber> Create(string value)
        {

            if (string.IsNullOrWhiteSpace(value))
                return Result.Failure<InventoryNumber>("Inventory number is required.");

            if (value.Length > MAX_INVENTORYNUMBER_LENGTH && value.Length < MIN_INVENTORYNUMBER_LENGTH)
                return Result.Failure<InventoryNumber>($"Inventory number must be between {MIN_INVENTORYNUMBER_LENGTH} and {MAX_INVENTORYNUMBER_LENGTH} characters.");

            if (!Regex.IsMatch(value, @"^[a-zA-Z0-9\-\/\_]+$"))
                return Result.Failure<InventoryNumber>("Inventory number contains invalid characters.");

            return new InventoryNumber(value);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}

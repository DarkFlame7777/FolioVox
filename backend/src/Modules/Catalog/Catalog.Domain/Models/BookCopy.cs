using CSharpFunctionalExtensions;

namespace Catalog.Domain.Models
{
    public sealed class BookCopy : Entity<BookCopyId>
    {
        public BookId BookId { get; }
        public InventoryNumber InventoryNumber { get; }

        private BookCopy(BookCopyId id, BookId bookId, InventoryNumber inventoryNumber) : base(id)
        {
            BookId = bookId;
            InventoryNumber = inventoryNumber;
        }

        public static Result<BookCopy> Create(BookCopyId bookCopyId, BookId bookId, InventoryNumber inventoryNumber)
            => new BookCopy(bookCopyId, bookId, inventoryNumber);
        
    }
}

using BooksInventoryCodeFirst.Data;
using BooksInventoryCodeFirst.Models;
using Microsoft.EntityFrameworkCore;

namespace BooksInventoryCodeFirst.Services
{
    public class BookInventoryService
    {
        private readonly BookInventoryContext _context;

        public BookInventoryService(BookInventoryContext context)
        {
            _context = context;
        }

        public List<Book> GetAllBooks()
        {
            return _context.Books
                .AsNoTracking()
                .OrderBy(book => book.Name)
                .ToList();
        }

        public Book? GetBookByIsbn(string isbn)
        {
            return _context.Books.Find(isbn);
        }

        public void AddBook(Book book)
        {
            _context.Books.Add(book);
            _context.SaveChanges();
        }

        public void UpdateBook(Book book)
        {
            _context.Books.Update(book);
            _context.SaveChanges();
        }

        public void DeleteBook(string isbn)
        {
            Book? book = GetBookByIsbn(isbn);
            if (book is null)
            {
                return;
            }

            _context.Books.Remove(book);
            _context.SaveChanges();
        }
    }
}

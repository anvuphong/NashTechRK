using Microsoft.EntityFrameworkCore.Storage;
using MidAssignment.Data;
using MidAssignment.Entities;
using MidAssignment.Interfaces;

namespace MidAssignment.Services
{
    public class BookServices : ILibraryServices<Book>
    {
        private readonly LibraryContext _context;
        private readonly IDbContextTransaction _transaction;
        public BookServices(LibraryContext context)
        {
            _context = context;
            _transaction = _context.Database.BeginTransaction();
        }
        public void Add(Book book)
        {
            Transaction(book =>
            {
                _context.Books?.Add(book);
            }, book);
        }

        public List<Book> GetAll()
        {
            return _context.Books.ToList();
        }

        public Book GetById(int id)
        {
            return _context.Books?.FirstOrDefault(b => b.BookId == id);
        }

        public void Remove(Book book)
        {
            Transaction(book =>
            {
                _context.Books?.Remove(book);
            }, book);
        }

        public void Update(Book book)
        {
            var updateBook = _context.Books?.FirstOrDefault(b => b.BookId == book.BookId);
            updateBook.BookName = book.BookName;
            updateBook.Author = book.Author;
            updateBook.CategoryId = book.CategoryId;
            Transaction(book =>
            {
                _context.Books?.Update(book);
            }, updateBook);
        }

        public bool IsValidForeignKey(int? id)
        {
            return _context.Categories.Any(c => c.CategoryId == id);
        }

        public void Transaction(Action<Book> action, Book item)
        {
            try
            {
                action(item);
                _context.SaveChanges();
                _transaction.Commit();
            }
            catch (System.Exception)
            {
                _transaction.Rollback();
            }
        }
    }
}
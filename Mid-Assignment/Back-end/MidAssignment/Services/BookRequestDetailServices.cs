using Microsoft.EntityFrameworkCore.Storage;
using MidAssignment.Data;
using MidAssignment.Entities;
using MidAssignment.Interfaces;

namespace MidAssignment.Services
{
    public class BookRequestDetailServices : IDetailServices
    {
        private readonly LibraryContext _context;
        private readonly IDbContextTransaction _transaction;

        public BookRequestDetailServices(LibraryContext context)
        {
            _context = context;
            _transaction = _context.Database.BeginTransaction();
        }

        public void Add(BookRequestDetail detail)
        {
            Transaction(item =>
            {
                _context.BookRequestDetails?.Add(item);
            }, detail);
        }

        public List<BookRequestDetail> GetRequestsById(int id)
        {
            return _context.BookRequestDetails.Where(d => d.RequestId == id).ToList();
        }

        public bool IsValidForeignKey(int id)
        {
            return _context.Books.Any(b => b.BookId == id);
        }

        public void Remove(int id)
        {

            Transaction(item =>
                {
                    var listRequest = _context.BookRequestDetails.Where(d => d.RequestId == id);
                    foreach (BookRequestDetail detail in listRequest)
                    {
                        _context.BookRequestDetails.Remove(detail);
                    }
                }, null);


        }

        public void Transaction(Action<BookRequestDetail> action, BookRequestDetail? item)
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
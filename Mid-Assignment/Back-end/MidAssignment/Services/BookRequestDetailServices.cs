using AutoMapper;
using Microsoft.EntityFrameworkCore.Storage;
using MidAssignment.Data;
using MidAssignment.DTO;
using MidAssignment.Entities;
using MidAssignment.Interfaces;

namespace MidAssignment.Services
{
    public class BookRequestDetailServices : IDetailServices
    {
        private readonly LibraryContext _context;
        public BookRequestDetailServices(LibraryContext context)
        {
            _context = context;
        }

        public void Add(BookRequestDetail detail)
        {
            Transaction(item =>
            {
                _context.BookRequestDetails?.Add(item);
            }, detail);
        }

        public List<BookRequestDetailWithBookNameDTO> GetRequestsById(int id)
        {
            var requestListWithBookName = new List<BookRequestDetailWithBookNameDTO>();
            var requestList = _context.BookRequestDetails.Where(d => d.RequestId == id).ToList();
            foreach (var request in requestList)
            {
                var bookName = _context.Books.First(book => book.BookId == request.BookId).BookName;
                requestListWithBookName.Add(new BookRequestDetailWithBookNameDTO{
                    RequestId=request.RequestId,
                    BookId = request.BookId,
                    BookName = bookName
                });
            }
            return requestListWithBookName;
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
            var transaction = _context.Database.BeginTransaction();
            try
            {
                action(item);
                _context.SaveChanges();
                transaction.Commit();
            }
            catch (System.Exception)
            {
                transaction.Rollback();
            }
        }
    }
}
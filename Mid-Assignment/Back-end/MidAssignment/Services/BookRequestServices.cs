using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using MidAssignment.Data;
using MidAssignment.Entities;
using MidAssignment.Interfaces;

namespace MidAssignment.Services
{
    public class BookRequestServices : IRequestService
    {
        private readonly LibraryContext _context;
        private readonly IDbContextTransaction _transaction;

        public BookRequestServices(LibraryContext context)
        {
            _context = context;
            _transaction = _context.Database.BeginTransaction();
        }
        public void Add(BookRequest bookRequest)
        {
            Transaction(item =>
            {
                _context.BookRequests?.Add(item);
            }, bookRequest);
            _context.Database.UseTransaction(null);
        }

        public List<BookRequest> GetAll()
        {
            return _context.BookRequests.ToList();
        }

        public BookRequest GetById(int id)
        {
            return _context.BookRequests.FirstOrDefault(br => br.RequestByUserId == id);
        }

        public List<BookRequest> GetByUserId(int id)
        {
            return _context.BookRequests.Where(br => br.RequestByUserId == id).ToList();
        }

        public bool IsValidForeignKey(int? id)
        {
            return _context.Users.Any(u => u.UserId == id);
        }

        public void Remove(BookRequest bookRequest)
        {
            Transaction(item =>
            {
                _context.BookRequests?.Remove(item);
            }, bookRequest);
        }

        public void Transaction(Action<BookRequest> action, BookRequest item)
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

        public void Update(BookRequest bookRequest)
        {
            var updateRequest = _context.BookRequests?.FirstOrDefault(br => br.RequestId == bookRequest.RequestId);
            updateRequest.Status = bookRequest.Status;
            updateRequest.ProcessByUserId = bookRequest.ProcessByUserId;
            Transaction(bookRequest =>
            {
                _context.BookRequests?.Update(bookRequest);
            }, updateRequest);
        }
    }
}
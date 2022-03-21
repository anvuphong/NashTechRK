using MidAssignment.Entities;

namespace MidAssignment.Interfaces
{
    public interface IRequestService
    {
        List<BookRequest> GetAll();
        public List<BookRequest> GetByUserId(int id);
        public BookRequest GetById(int id);

        public void Add(BookRequest item);

        public void Update(BookRequest item);

        public void Remove(BookRequest item);

        public bool IsValidForeignKey(int? id);

        public void Transaction(Action<BookRequest> action, BookRequest item);

    }
}
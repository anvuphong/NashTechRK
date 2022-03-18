using MidAssignment.Entities;

namespace MidAssignment.Interfaces
{
    public interface IDetailServices
    {
        public List<BookRequestDetail> GetRequestsById(int id);

        public void Add(BookRequestDetail item);
        
        public void Remove(int id);

        public bool IsValidForeignKey(int id);

        public void Transaction(Action<BookRequestDetail> action, BookRequestDetail detail);

    }
}
using MidAssignment.DTO;
using MidAssignment.Entities;

namespace MidAssignment.Interfaces
{
    public interface IDetailServices
    {
        public List<BookRequestDetailWithBookNameDTO> GetRequestsById(int id);

        public void Add(BookRequestDetail detail);
        
        public void Remove(int id);

        public bool IsValidForeignKey(int id);

        public void Transaction(Action<BookRequestDetail> action, BookRequestDetail detail);

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MidAssignment.Entities;

namespace MidAssignment.Services
{
    public interface IBookServices
    {
        public List<Book> GetAllBook();
        public Book GetBookById();
        public void CreateNewBook();
        public void UpdateBook();
        public void DeleteBook();
    }
}
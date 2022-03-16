using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MidAssignment.Entities;

namespace MidAssignment.Interfaces
{
    public interface IBookRequestServices
    {
        public void CreateBookRequest();
        public void DeleteBookRequest();
        public BookRequest GetBookRequestById();
        public void ChangeStatusBookRequest();

    }
}
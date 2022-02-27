using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment2.DTO
{
    public class CategoryDTO
    {
        public string CategoryName { get; set; }
    }

    public class CategoryWithIdDTO : CategoryDTO
    {
        public int CategoryId { get; set; }
    }
}
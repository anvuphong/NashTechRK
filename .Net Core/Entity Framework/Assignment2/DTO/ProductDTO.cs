using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment2.DTO
{
    public class ProductDTO
    {
        public string ProductName { get; set; }
        public string Manufacture { get; set; }
        public int CategoryId { get; set; }
    }
    public class ProductWithIdDTO : ProductDTO
    {
        public int ProductId { get; set; }
    }
}
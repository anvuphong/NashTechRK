using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MidAssignment.Entities;

namespace MidAssignment.Interfaces
{
    public interface ICategoryServices
    {
        public List<Category> GetAllCategories();
        public Category GetCategoriesById();
        public void CreateNewCategory();
        public void UpdateCategory();
        public void DeleteCategory();
    }
}
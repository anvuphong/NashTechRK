using Microsoft.EntityFrameworkCore.Storage;
using MidAssignment.Data;
using MidAssignment.Entities;
using MidAssignment.Interfaces;

namespace MidAssignment.Services
{
    public class CategoryServices : ILibraryServices<Category>
    {
        private readonly LibraryContext _context;
        private readonly IDbContextTransaction _transaction;
        public CategoryServices(LibraryContext context)
        {
            _context = context;
            _transaction = _context.Database.BeginTransaction();
        }

        public void Add(Category category)
        {
            Transaction(category =>
            {
                _context.Categories?.Add(category);
            }, category);
        }

        public List<Category> GetAll()
        {
            return _context.Categories.ToList();
        }

        public Category GetById(int id)
        {
            return _context.Categories.FirstOrDefault(x => x.CategoryId == id);
        }

        public bool IsValidForeignKey(int? id)
        {
            throw new NotImplementedException();
        }

        public void Remove(Category category)
        {
            Transaction(category =>
            {
                _context.Categories?.Remove(category);
            }, category);
        }

        public void Transaction(Action<Category> action, Category item)
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

        public void Update(Category category)
        {
            var updateCategory = _context.Categories?.FirstOrDefault(x => x.CategoryId == category.CategoryId);
            updateCategory.CategoryName = category.CategoryName;
            Transaction(category =>
            {
                _context.Categories.Update(category);
            }, updateCategory);
        }
    }
}
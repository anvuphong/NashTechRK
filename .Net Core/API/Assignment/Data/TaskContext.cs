using Microsoft.EntityFrameworkCore;
using Assignment.Entities;

namespace Assignment.Data
{
    public class TaskContext : DbContext
    {
        public TaskContext(DbContextOptions<TaskContext> options) : base(options) { }
        public DbSet<ToDoTask> Tasks { get; set; }
    }
}
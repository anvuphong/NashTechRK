using Assignment1.Entities;
using Microsoft.EntityFrameworkCore;

namespace Assignment1.Data
{
    public class StudentContext : DbContext
    {
        public StudentContext(DbContextOptions<StudentContext> options) : base(options) { }
        public DbSet<Student> Student { get; set; }
    }
}
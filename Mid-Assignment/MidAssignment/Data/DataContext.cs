using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MidAssignment.Entities;

namespace MidAssignment.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> option) : base(option) { }
        public DbSet<User>? Users { get; set; }
        public DbSet<Book>? Books { get; set; }
        public DbSet<Category>? Categories { get; set; }
        public DbSet<BookRequest>? BookRequests { get; set; }
        public DbSet<BookRequestDetail>? BookRequestDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                        .HasMany<BookRequest>(br => br.BookRequests)
                        .WithOne(u => u.User)
                        .HasForeignKey(s => s.UserId)
                        .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Category>()
                        .HasMany<Book>(b => b.Books)
                        .WithOne(c => c.Category)
                        .HasForeignKey(s => s.CategoryId)
                        .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<BookRequest>()
                        .HasMany<BookRequestDetail>(brd => brd.BookRequestDetails)
                        .WithOne(br => br.BookRequest)
                        .HasForeignKey(s => s.RequestId)
                        .OnDelete(DeleteBehavior.Cascade);
            
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MidAssignment.Entities;
using MidAssignment.Enums;

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
            //Relationship between User and BookRequest
            modelBuilder.Entity<User>()
                        .HasMany<BookRequest>(br => br.BookRequests)
                        .WithOne(u => u.RequestBy)
                        .HasForeignKey(frk => frk.RequestByUserId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>()
                        .HasMany<BookRequest>(br => br.BookProcessRequests)
                        .WithOne(u => u.ProcessBy)
                        .HasForeignKey(frk => frk.ProcessByUserId)
                        .IsRequired(false)
                        .OnDelete(DeleteBehavior.SetNull);

            //Relationship between Category and Book
            modelBuilder.Entity<Category>()
                        .HasMany<Book>(b => b.Books)
                        .WithOne(c => c.Category)
                        .HasForeignKey(frk => frk.CategoryId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Cascade);

            //BookRequestDetail
            modelBuilder.Entity<BookRequestDetail>().HasKey(d => new { d.RequestId, d.BookId });

            modelBuilder.Entity<BookRequest>()
                        .HasMany<BookRequestDetail>(brd => brd.Details)
                        .WithOne(br => br.BookRequest)
                        .HasForeignKey(frk => frk.RequestId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Cascade);
                        
            modelBuilder.Entity<Book>()
                        .HasMany<BookRequestDetail>(brd => brd.Details)
                        .WithOne(br => br.Book)
                        .HasForeignKey(frk => frk.BookId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>().HasData(
                new User { UserId = 1, UserName = "mrA", Password = "12345678", RoleId = 1 },
                new User { UserId = 2, UserName = "mrB", Password = "12345678", RoleId = 1 },
                new User { UserId = 3, UserName = "mrC", Password = "12345678", RoleId = 2 },
                new User { UserId = 4, UserName = "mrD", Password = "12345678", RoleId = 2 },
                new User { UserId = 5, UserName = "mrE", Password = "12345678", RoleId = 2 }
            );
            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = 1, CategoryName = "Detective Book" },
                new Category { CategoryId = 2, CategoryName = "Comic" },
                new Category { CategoryId = 3, CategoryName = "Novel" }
            );
            modelBuilder.Entity<Book>().HasData(
                new Book { BookId = 1, BookName = " Sherlock Holmes", Author = "Conan Doyle", CategoryId = 1 },
                new Book { BookId = 2, BookName = "Detective", Author = "Hailey", CategoryId = 1 },
                new Book { BookId = 3, BookName = "CODA", Author = "Simon Spurrier", CategoryId = 2 },
                new Book { BookId = 4, BookName = "MARVELS", Author = "Kurt Busiek", CategoryId = 2 },
                new Book { BookId = 5, BookName = "Dracula", Author = "Bram Stoker", CategoryId = 3 },
                new Book { BookId = 6, BookName = "The Hobbit", Author = "J. R. R. Tolkien", CategoryId = 3 }
            );
            modelBuilder.Entity<BookRequest>().HasData(
                new BookRequest { RequestId = 1, RequestByUserId = 3, DateOfRequest = (new DateTime(2021, 08, 09)), Status = Status.Waiting },
                new BookRequest { RequestId = 2, RequestByUserId = 3, DateOfRequest = (new DateTime(2022, 02, 09)), Status = Status.Approved, ProcessByUserId = 1 },
                new BookRequest { RequestId = 3, RequestByUserId = 4, DateOfRequest = (new DateTime(2022, 03, 10)), Status = Status.Rejected, ProcessByUserId = 2 }
            );
            modelBuilder.Entity<BookRequestDetail>().HasData(
                new BookRequestDetail { RequestId = 1, BookId = 1 },
                new BookRequestDetail { RequestId = 1, BookId = 2 },
                new BookRequestDetail { RequestId = 1, BookId = 3 },
                new BookRequestDetail { RequestId = 2, BookId = 3 },
                new BookRequestDetail { RequestId = 2, BookId = 4 },
                new BookRequestDetail { RequestId = 3, BookId = 5 },
                new BookRequestDetail { RequestId = 3, BookId = 6 }
            );
        }
    }
}
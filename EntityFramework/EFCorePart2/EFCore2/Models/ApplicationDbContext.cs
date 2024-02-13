using System;
using System.Collections.Generic;
using EFCore2.Dtos;
using Microsoft.EntityFrameworkCore;

namespace EFCore2.Models;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Blog> Blogs { get; set; }

    public virtual DbSet<Post> Posts { get; set; }

    public virtual DbSet<Book> Books { get; set; }
    public virtual DbSet<Author> Authors { get; set; }
    public virtual DbSet<Nationality> Nationalities { get; set; }
    public virtual DbSet<BookDto> BooksDto { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseLazyLoadingProxies().UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=EFCore2;Integrated Security=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // one-many relationship Author to Book
        modelBuilder.Entity<Book>()
            .HasOne(b => b.Author)
            .WithMany(a => a.Books)
            .HasForeignKey(b => b.AuthorId);


        // one-many relationship Nationality to Author
        modelBuilder.Entity<Author>()
            .HasOne(a => a.Nationality)
            .WithMany(n => n.Authors)
            .HasForeignKey(a => a.NationalityId);


        modelBuilder.Entity<Author>()
            .Property<int>("NationalityId").
            HasColumnName("NationalityId");


        modelBuilder.Entity<Blog>()
            .HasQueryFilter(b => b.Posts.Count > 0);


        modelBuilder.Entity<Blog>()
            .HasMany(b => b.Posts)
            .WithOne(p => p.Blog)
            .OnDelete(DeleteBehavior.SetNull);
    }


    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    public class Book
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal Price { get; set; }

        // Foreign Key
        public int AuthorId { get; set; } 
        public virtual Author? Author { get; set; }

        public override string ToString()
        {
            return base.ToString();
        }
    }

    public class Author
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        

        // Foreign Key
        public int NationalityId { get; set; }
        public virtual Nationality? Nationality { get; set; }

        public virtual List<Book>? Books { get; set; }
       // public Book Book { get; set; }
    }

    public  class Nationality
    {
        public int NationalityId { get; set; }
        public string? Name { get; set; }

        public virtual List<Author>? Authors { get; set; }
        public virtual Author? Author { get; set; }
    }
}

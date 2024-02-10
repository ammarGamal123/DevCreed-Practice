using EFCore2.Models;
using System.Diagnostics;
using System.Linq;
using System.Collections.Generic;
using static EFCore2.Models.ApplicationDbContext;

namespace EFCore2
{
    public class Program
    {
        static void Main(string[] args)
        {
            var context = new ApplicationDbContext();


            var books = context.Books
                .Join(
                    context.Authors, // which table i I have the relationship with
                    book => book.AuthorId,
                    author => author.Id,

                    (book , author) => new
                    {
                        bookId = book.Id,
                        bookName = book.Name,
                        authorName = author.Name,
                        authorNationalityId = author.NationalityId
                    }
                    )
                .Join(
                    context.Nationalities, // which table i I have the relationship with
                    book => book.authorNationalityId,
                    nationality => nationality.NationalityId,
                    (book , nationality) => new
                    {
                        book.bookName,
                        book.bookId,
                        book.authorName,
                        authorNationalityName =  nationality.Name,
                    }
                    )
                .ToList();
            
            foreach(var book in books)
            {
                Console.WriteLine($"Book Id = {book.bookId} , " +
                    $"Book Name = {book.bookName} , " +
                    $"Author Name  = {book.authorName} , " +
                    $"Author Nationality Name = {book.authorNationalityName}");
            }

        }

    }
}

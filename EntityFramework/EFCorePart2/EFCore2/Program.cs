using EFCore2.Models;
using System.Diagnostics;
using System.Linq;
using System.Collections.Generic;
using static EFCore2.Models.ApplicationDbContext;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using Microsoft.Data.SqlClient;
using EFCore2.Dtos;
using System.Net.Mime;

namespace EFCore2
{
    public class Program
    {
        static void Main(string[] args)
        {
            var context = new ApplicationDbContext();


            #region Maxby And Minby
            
            var cheapest = context.Books.ToList().MinBy(b => b.Price);
            Console.WriteLine(cheapest?.Name + " , " + cheapest?.Price);

            var mostExpesive = context.Books.ToList().MaxBy(b => b.Price);
            Console.WriteLine(mostExpesive?.Name + " , " + mostExpesive?.Price);
            
            #endregion

            #region Save Data With Sql Statement
            context.Database
                .ExecuteSqlRaw(
                "INSERT INTO Blogs (Blogs.Url , Blogs.AddedOn) VALUES ('Test' , GETDATE()) "
                );

            #endregion

            #region Transaction And RollBack

            using var transaction = context.Database.BeginTransaction();

            try{
                context.Blogs.Add(new Blog { Url = "www.test1.com" });
                context.SaveChanges();

                transaction.CreateSavepoint("Add First Blog");

                context.Blogs.Add(new Blog { Url = "www.test2.com" });
                context.Blogs.Add(new Blog {BlogId = 5, Url = "www.test5.com" });
                context.SaveChanges();
                context.Blogs.Add(new Blog { Url = "www.test3.com" });
                context.SaveChanges();
                context.Blogs.Add(new Blog { Url = "www.test4.com" });
                context.SaveChanges();

                transaction.Commit();

            }
            catch (System.Exception) { 
                transaction.RollbackToSavepoint("Add First Blog");
                transaction.Commit(); 
            }

            
            #endregion

            #region Remove And RemoveRange


            // Remove && RemoveRange
            
             * var blog = context.Blogs.Find(3);

            var posts = context.Posts.Where(p => p.BlogId == 3).ToList();

            context.RemoveRange(posts);

            context.Remove(blog);

            context.Update(blog);

            context.SaveChanges();


            
            var post = context.Posts.Where(p => p.PostId >= 3 && p.PostId <= 4).ToList();
            context.Posts.RemoveRange(post);
            





            // UpdateRange
        
            var posts = context.Posts.Where(p => !p.IsDeleted).ToList();
            
            foreach(var post in posts)
            {
                post.IsDeleted = true;
            }
            context.UpdateRange(posts);
            context.SaveChanges();
            
            // How to Update a specific column not the other columns in the same row 
            var post = new Post
            {
                PostId = 5,
                BlogId = 4,
                Content = "Updated Content",
                IsDeleted = true,
            };

            context.Update(post);
            context.Entry(post).Property(p => p.BlogId).IsModified = false;
            context.Entry(post).Property(p => p.Title).IsModified = false;



            #endregion

            #region Update And UpdateRange


            var currentNationality = context.Nationalities.Find(2);
            var newNationality = new Nationality { NationalityId = 2, Name = "French" };

            context.Entry(currentNationality).CurrentValues.SetValues(newNationality);


            var nationality = new Nationality { NationalityId = 2, Name = "Turkish" };
            context.Update(nationality);
            

            #endregion

            #region Save New Data in the Database
             var blog = new Blog
             {
                 Url = "www.google.com",

                 Posts = new List<Post>
                 {
                     new Post
                     {
                         Title = "How to center a div in html",
                         Content = "Please Ask ChatGPT"
                     }
                 }

             };

             context.Blogs.Add(blog);
             context.SaveChanges();
             
            #endregion

            #region QueryFilter 
            var blogs = context.Blogs.IgnoreQueryFilters().ToList();
            foreach(var blog in blogs)
            {
                Console.WriteLine(blog.Url);
            }


            var posts = context.Posts.ToList();
            foreach(var post in posts )
            {
                Console.WriteLine($"Name: {post.Title} , Content: {post.Content}");
            }
            #endregion

            #region Insert New Data
            var blogs = new List<Blog>
            {
                new Blog {Url = "www.facebook.com"},
                new Blog {Url = "www.twitter.com"},
                new Blog {Url = "www.leetcode.com"},
                new Blog {Url = "www.meduim.com"},
                new Blog {Url = "www.reddit.com"},
                new Blog {Url = "www.qoura.com"},
                new Blog {Url = "www.codeforces..com"},
                new Blog {Url = "www.intagram.com"},
                new Blog {Url = "www.tiktok.com"},
                new Blog {Url = "www.whatsapp.com"},
            };
            

            var posts = new List<Post>
            {
                new Post {Title = "How to Solve Hard Problem" ,
                          Content = "you have to be strong",
                          BlogId = 3
                },

                new Post {Title = "How to Solve 1500 Problem" ,
                          Content = "you have to be good",
                          BlogId = 7
                },

                new Post {Title = "A Launch of a new Course" ,
                          Content = "How to Program From Zero",
                          BlogId = 1
                },

                new Post {Title = "why software engineers are genuis" ,
                          Content = "the study alot of time and can deal with computers",
                          BlogId = 6
                }
            };

            context.AddRange(posts);
            context.SaveChanges();

            #endregion

            #region SQL Statement
            var bookId = new SqlParameter("Id" , 1);
            var book = context.Books.FromSqlRaw($"prc_GetBookAllBooks").ToList();
            foreach( var item in book )
            {
                Console.WriteLine(item.Name);
            }
            var book = context.Books.FromSqlRaw("SELECT * FROM Books").ToList();

            foreach(var b in book)
            {
                Console.WriteLine($"Id = {b.Id} , Name = {b.Name} , Price = {b.Price}");
            }
            #endregion


            #region Eager Loading
            // Eager Loading
            var book = context.Books.SingleOrDefault(b => b.Id == 1);
            Console.WriteLine(book.Author.Nationality.Name);

            var book = (from b in context.Books
                       join a in context.Authors
                       on b.AuthorId equals a.Id
                       join n in context.Nationalities
                       on a.NationalityId equals n.NationalityId into authorNationality
                       from an in authorNationality.DefaultIfEmpty()

                       select new
                       {
                           BookId = b.Id,
                           BookName = b.Name,
                           AuthorName = a.Name,
                           //NationalityName = n.Name
                       }).Count();


            Console.WriteLine(book);


             // Include is called Eager Loading
             var book = context.Books
                 .Include(b => b.Author)
                 .ThenInclude(a => a.Nationality)
                 .SingleOrDefault(b => b.Id == 1);

             Console.WriteLine(book.Author.Nationality.Name);

            
                        var book = context.Books.SingleOrDefault(b => b.Id == 1);

                        context.Entry(book).Reference(b => b.Author).Load();
                        Console.WriteLine(book.Author.Name); 

            var nationality = context.Nationalities.SingleOrDefault(a => a.NationalityId == 1);

            context.Entry(nationality)
                .Collection(n => n.Authors)
                .Query()
                .Where(n => n.NationalityId == 1)
                .ToList();

            foreach(var auth in context.Authors)
            {
                Console.WriteLine(auth.Name);
            }

            #endregion

        }

    }
}

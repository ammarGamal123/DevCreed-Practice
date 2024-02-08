using EFCore2.Models;
using EFCore2.Models;

namespace EFCore2
{
    public class Program
    {
        static void Main(string[] args)
        {
            var context = new ApplicationDbContext();

            var blog = context.Blogs.SingleOrDefault(b => b.BlogId == 10);

            Console.WriteLine(blog == null ? "No Items Found" : $"{blog.BlogId} , {blog.Url}");
       
        }
        public static void SeedData()
        {
            using (var context = new ApplicationDbContext())
            {
                context.Database.EnsureCreated();

                var blog = context.Blogs.FirstOrDefault(b => b.Url
                == "www.twitter.com");
               
                if (blog == null) {
                    context.Blogs.Add(new Models.
                        Blog { Url = "www.twitter.com" });
                }
                context.SaveChanges();
            }
        }

        public static void SeedData2()
        {
            using (var context = new ApplicationDbContext())
            {
                context.Database.EnsureCreated();

                var blog = context.Blogs.FirstOrDefault(b => b.Url
                == "www.codeforces");



                if (blog == null)
                {
                    context.Blogs.Add(new Models.Blog { Url = "www.codeforces" +
                        ".com" });
                }
                context.SaveChanges();
            }
        }
    }

    
}

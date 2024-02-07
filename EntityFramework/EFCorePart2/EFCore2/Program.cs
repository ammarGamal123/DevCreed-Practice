//using EFCore2.Models;
namespace EFCore2
{
    public class Program
    {
        static void Main(string[] args)
        {
            //  var _context = new ApplicationDbContext();


            //  _context.SaveChanges();


            SeedData();
            
            Console.ReadKey();
        }

        public static void SeedData()
        {
            using (var context = new ApplicationDbContext())
            {
                context.Database.EnsureCreated();

                var blog = context.Blogs.FirstOrDefault(b => b.Url == "www.google.com");

                if (blog == null)
                {
                    context.Blogs.Add(new Blog { Url = "www.google.com" });
                }
                context.SaveChanges();
            }
        }
    }
}

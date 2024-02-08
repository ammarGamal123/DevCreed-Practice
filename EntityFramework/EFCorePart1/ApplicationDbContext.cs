using EFCore.Configurations;
using EFCore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore
{
    public class ApplicationDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder options) =>
            options.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=EFCore;Integrated Security=True");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AuditEntry>();
            new BlogEntityTypeConfiguration().Configure(modelBuilder.Entity<Blog>());
            // this line is equivelant for the line below 

            //    modelBuilder.ApplyConfigurationsFromAssembly(typeof(BlogEntityTypeConfiguration).Assembly)

            /*modelBuilder.Entity<Blog>()
                .ToTable("Blogs", b => b.ExcludeFromMigrations());*/

            /*modelBuilder.Entity<Post>()
                .ToTable("Posts");*/

            /*modelBuilder.Entity<Post>()
                .ToTable("Posts" , schema : "blogging");
*/
            /*modelBuilder.Entity<Post>()
                .ToView("SelectPosts", schema: "blogging");*/

            /*modelBuilder.HasDefaultSchema("blogging");*/

            /*modelBuilder.Entity<Blog>()
                .Ignore(b => b.AddedOn);*/

            /*modelBuilder.Entity<Blog>()
                .Property(b => b.Url)
                .HasColumnName("BlogUrl");*/

            /*modelBuilder.Entity<Blog>(eb =>
            {
                eb.Property(b => b.Url).HasColumnType("varchar(200)");
                eb.Property(b => b.Rating).HasColumnType("decimal(5,2)");
            });*/

            /*modelBuilder.Entity<Blog>(eb =>
            {
                eb.Property(b => b.Url).HasMaxLength(200);
            });*/

            /* modelBuilder.Entity<Blog>(eb =>
             {
                 eb.Property(b => b.Url).HasComment("The Url for the Blog");
             });

             modelBuilder.Entity<Book>(eb =>
             {
                 eb.HasKey(b => b.BookKey);
             });*/

            /*modelBuilder.Entity<Book>(eb =>
            {
                eb.HasKey(b => b.BookKey).HasName("PK_BookKey");
            });*/


            // Add Composite Key (Composite Primary Key)
            modelBuilder.Entity<Book>(eb =>
            {
                eb.HasKey(b => new { b.Name, b.Author }); // Anonymous Object
            });
        }
        public DbSet<Blog> blogs { get; set; }

        public DbSet<Book> books { get; set; }

    }
}
=======
﻿using EFCore.Configurations;
using EFCore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore
{
    public class ApplicationDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder options) =>
            options.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=EFCore;Integrated Security=True");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            new BlogEntityTypeConfiguration().Configure(modelBuilder.Entity<Blog>());
            // this line is equivelant for the line below 

        //    modelBuilder.ApplyConfigurationsFromAssembly(typeof(BlogEntityTypeConfiguration).Assembly)


        }
        public DbSet<Blog> blogs { get; set; }
    }
}
>>>>>>> origin/main

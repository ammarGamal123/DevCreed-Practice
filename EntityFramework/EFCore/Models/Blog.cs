using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore.Models
{
    public class Blog
    {
         public int Id { get; set; }

        // [Column(TypeName = "varchar(200)")]
        // [Comment ("The Url for the Blog")]
        public string Url { get; set; }

        // [Column(TypeName = "decimal(5 , 2)")]
        

        public List <Post> Posts { get; set; }
        
            

    }
}

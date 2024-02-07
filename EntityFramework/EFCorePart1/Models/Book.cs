using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore.Models
{
    public class Book
    {
       // [Key]
        public int BookKey { get; set; }
        public string Name { get; set; }
        
        public string Author { get; set; }

    }
}

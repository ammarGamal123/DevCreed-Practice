
using System.ComponentModel.DataAnnotations.Schema;

namespace Movies.api.Models
{
    public class Genre
    {
        // To Make Id Auto Incremented Starting From 1
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public byte Id { get; set; }

        [MaxLength(80)]
        public string Name { get; set; }
        
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EFCore2.Models;

public partial class Blog
{
    [Key]
    public int BlogId { get; set; }

    public string Url { get; set; } = null!;

    public DateTime AddedOn { get; set; }

    [InverseProperty("Blog")]
    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();
}

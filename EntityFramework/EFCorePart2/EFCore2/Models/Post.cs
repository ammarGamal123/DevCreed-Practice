using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EFCore2.Models;

[Index("BlogId", Name = "IX_Posts_BlogId")]
public partial class Post
{
    [Key]
    public int PostId { get; set; }

    public string Title { get; set; } = null!;

    public string Content { get; set; } = null!;

    public int? BlogId { get; set; }

    [ForeignKey("BlogId")]
    [InverseProperty("Posts")]
    public virtual Blog Blog { get; set; } = null!;

    public bool IsDeleted { get; set; }
}

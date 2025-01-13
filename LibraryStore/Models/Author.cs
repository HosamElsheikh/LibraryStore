using System;
using System.Collections.Generic;

namespace LibraryStore.Models;

public partial class Author
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int Age { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}

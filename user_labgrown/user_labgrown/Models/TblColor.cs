using System;
using System.Collections.Generic;

namespace user_labgrown.Models;

public partial class TblColor
{
    public int Id { get; set; }

    public string Color { get; set; } = null!;

    public virtual ICollection<TblProduct> TblProducts { get; set; } = new List<TblProduct>();
}

using System;
using System.Collections.Generic;

namespace admin_labgrown.Models;

public partial class TblShape
{
    public int Id { get; set; }

    public string ShapeName { get; set; } = null!;

    public virtual ICollection<TblProduct> TblProducts { get; set; } = new List<TblProduct>();
}

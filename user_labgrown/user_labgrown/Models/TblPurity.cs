using System;
using System.Collections.Generic;

namespace user_labgrown.Models;

public partial class TblPurity
{
    public int Id { get; set; }

    public string Purity { get; set; } = null!;

    public virtual ICollection<TblProduct> TblProducts { get; set; } = new List<TblProduct>();
}

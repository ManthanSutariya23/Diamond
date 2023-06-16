using System;
using System.Collections.Generic;

namespace user_labgrown.Models;

public partial class TblOrder
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int ProductId { get; set; }

    public int Status { get; set; }

    public double Amount { get; set; }

    public virtual TblProduct Product { get; set; } = null!;

    public virtual TblRegister User { get; set; } = null!;
}

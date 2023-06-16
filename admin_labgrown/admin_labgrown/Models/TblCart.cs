using System;
using System.Collections.Generic;

namespace admin_labgrown.Models;

public partial class TblCart
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int ProductId { get; set; }

    public double Price { get; set; }

    public int Qty { get; set; }

    public double Subtotal { get; set; }

    public virtual TblProduct Product { get; set; } = null!;

    public virtual TblRegister User { get; set; } = null!;
}

using System;
using System.Collections.Generic;

namespace admin_labgrown.Models;

public partial class TblRegister
{
    public int Id { get; set; }

    public string FName { get; set; } = null!;

    public string LName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Address1 { get; set; } = null!;

    public string? Address2 { get; set; }

    public string Postcode { get; set; } = null!;

    public string City { get; set; } = null!;

    public string State { get; set; } = null!;

    public int CountryId { get; set; }

    public virtual TblCountry Country { get; set; } = null!;

    public virtual ICollection<TblCart> TblCarts { get; set; } = new List<TblCart>();

    public virtual ICollection<TblOrder> TblOrders { get; set; } = new List<TblOrder>();
}

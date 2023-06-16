using System;
using System.Collections.Generic;

namespace admin_labgrown.Models;

public partial class TblCountry
{
    public int Id { get; set; }

    public string CountryName { get; set; } = null!;

    public virtual ICollection<TblRegister> TblRegisters { get; set; } = new List<TblRegister>();
}

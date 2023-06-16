using System;
using System.Collections.Generic;

namespace admin_labgrown.Models;

public partial class TblProduct
{
    public int Id { get; set; }

    public int Shape { get; set; }

    public int Color { get; set; }

    public int Purity { get; set; }

    public int Fluorescence { get; set; }

    public int Certificate { get; set; }

    public string CutGrade { get; set; } = null!;

    public string Polish { get; set; } = null!;

    public string Symmetry { get; set; } = null!;

    public double Carat { get; set; }

    public virtual TblCertificate CertificateNavigation { get; set; } = null!;

    public virtual TblColor ColorNavigation { get; set; } = null!;

    public virtual TblFluorescence FluorescenceNavigation { get; set; } = null!;

    public virtual TblPurity PurityNavigation { get; set; } = null!;

    public virtual TblShape ShapeNavigation { get; set; } = null!;

    public virtual ICollection<TblCart> TblCarts { get; set; } = new List<TblCart>();

    public virtual ICollection<TblOrder> TblOrders { get; set; } = new List<TblOrder>();
}

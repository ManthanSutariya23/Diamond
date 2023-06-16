using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace user_labgrown.Models;

public partial class LabgrowndbContext : DbContext
{
    public LabgrowndbContext()
    {
    }

    public LabgrowndbContext(DbContextOptions<LabgrowndbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TblCart> TblCarts { get; set; }

    public virtual DbSet<TblCertificate> TblCertificates { get; set; }

    public virtual DbSet<TblColor> TblColors { get; set; }

    public virtual DbSet<TblCountry> TblCountries { get; set; }

    public virtual DbSet<TblFluorescence> TblFluorescences { get; set; }

    public virtual DbSet<TblOrder> TblOrders { get; set; }

    public virtual DbSet<TblProduct> TblProducts { get; set; }

    public virtual DbSet<TblPurity> TblPurities { get; set; }

    public virtual DbSet<TblRegister> TblRegisters { get; set; }

    public virtual DbSet<TblShape> TblShapes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TblCart>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_tbl_check_out");

            entity.ToTable("tbl_cart");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Price).HasColumnName("price");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.Qty).HasColumnName("qty");
            entity.Property(e => e.Subtotal).HasColumnName("subtotal");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Product).WithMany(p => p.TblCarts)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK_tbl_check_out_tbl_product");

            entity.HasOne(d => d.User).WithMany(p => p.TblCarts)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tbl_cart_tbl_register");
        });

        modelBuilder.Entity<TblCertificate>(entity =>
        {
            entity.ToTable("tbl_certificate");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CertificateName)
                .HasMaxLength(50)
                .HasColumnName("certificate_name");
        });

        modelBuilder.Entity<TblColor>(entity =>
        {
            entity.ToTable("tbl_color");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Color)
                .HasMaxLength(50)
                .HasColumnName("color");
        });

        modelBuilder.Entity<TblCountry>(entity =>
        {
            entity.ToTable("tbl_country");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CountryName)
                .HasMaxLength(50)
                .HasColumnName("country_name");
        });

        modelBuilder.Entity<TblFluorescence>(entity =>
        {
            entity.ToTable("tbl_fluorescence");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Fluorescence)
                .HasMaxLength(50)
                .HasColumnName("fluorescence");
        });

        modelBuilder.Entity<TblOrder>(entity =>
        {
            entity.ToTable("tbl_order");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Amount).HasColumnName("amount");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Product).WithMany(p => p.TblOrders)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tbl_order_tbl_product1");

            entity.HasOne(d => d.User).WithMany(p => p.TblOrders)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_tbl_order_tbl_register");
        });

        modelBuilder.Entity<TblProduct>(entity =>
        {
            entity.ToTable("tbl_product");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Carat).HasColumnName("carat");
            entity.Property(e => e.Certificate).HasColumnName("certificate");
            entity.Property(e => e.Color).HasColumnName("color");
            entity.Property(e => e.CutGrade)
                .HasMaxLength(50)
                .HasColumnName("cut_grade");
            entity.Property(e => e.Fluorescence).HasColumnName("fluorescence");
            entity.Property(e => e.Polish)
                .HasMaxLength(50)
                .HasColumnName("polish");
            entity.Property(e => e.Purity).HasColumnName("purity");
            entity.Property(e => e.Shape).HasColumnName("shape");
            entity.Property(e => e.Symmetry)
                .HasMaxLength(50)
                .HasColumnName("symmetry");

            entity.HasOne(d => d.CertificateNavigation).WithMany(p => p.TblProducts)
                .HasForeignKey(d => d.Certificate)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tbl_product_tbl_certificate");

            entity.HasOne(d => d.ColorNavigation).WithMany(p => p.TblProducts)
                .HasForeignKey(d => d.Color)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tbl_product_tbl_color");

            entity.HasOne(d => d.FluorescenceNavigation).WithMany(p => p.TblProducts)
                .HasForeignKey(d => d.Fluorescence)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tbl_product_tbl_fluorescence");

            entity.HasOne(d => d.PurityNavigation).WithMany(p => p.TblProducts)
                .HasForeignKey(d => d.Purity)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tbl_product_tbl_purity");

            entity.HasOne(d => d.ShapeNavigation).WithMany(p => p.TblProducts)
                .HasForeignKey(d => d.Shape)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tbl_product_tbl_shape");
        });

        modelBuilder.Entity<TblPurity>(entity =>
        {
            entity.ToTable("tbl_purity");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Purity)
                .HasMaxLength(50)
                .HasColumnName("purity");
        });

        modelBuilder.Entity<TblRegister>(entity =>
        {
            entity.ToTable("tbl_register");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Address1).HasColumnName("address_1");
            entity.Property(e => e.Address2).HasColumnName("address_2");
            entity.Property(e => e.City)
                .HasMaxLength(50)
                .HasColumnName("city");
            entity.Property(e => e.CountryId).HasColumnName("country_id");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.FName).HasColumnName("f_name");
            entity.Property(e => e.LName).HasColumnName("l_name");
            entity.Property(e => e.Password).HasColumnName("password");
            entity.Property(e => e.Postcode)
                .HasMaxLength(7)
                .IsFixedLength()
                .HasColumnName("postcode");
            entity.Property(e => e.State)
                .HasMaxLength(50)
                .HasColumnName("state");

            entity.HasOne(d => d.Country).WithMany(p => p.TblRegisters)
                .HasForeignKey(d => d.CountryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tbl_register_tbl_country");
        });

        modelBuilder.Entity<TblShape>(entity =>
        {
            entity.ToTable("tbl_shape");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ShapeName)
                .HasMaxLength(50)
                .HasColumnName("shape_name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Repository.Configurations;

namespace Repository.EFModels;

public partial class PropertyDatabaseContext : DbContext
{
    public PropertyDatabaseContext()
    {
    }

    public PropertyDatabaseContext(DbContextOptions<PropertyDatabaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<PropertyForSale> PropertyForSales { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer(AppConfigurations.ConnectionString);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PropertyForSale>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ID");

            entity.ToTable("propertyForSale");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Area)
                .IsUnicode(false)
                .HasColumnName("area");
            entity.Property(e => e.Description)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.Latitude)
                .HasColumnType("decimal(9, 6)")
                .HasColumnName("latitude");
            entity.Property(e => e.Location)
                .IsUnicode(false)
                .HasColumnName("location");
            entity.Property(e => e.Longitude)
                .HasColumnType("decimal(9, 6)")
                .HasColumnName("longitude");
            entity.Property(e => e.OwnerEmail)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Phone)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Price).HasColumnName("price");
            entity.Property(e => e.Title)
                .IsUnicode(false)
                .HasColumnName("title");

            entity.HasOne(d => d.OwnerEmailNavigation).WithMany(p => p.PropertyForSales)
                .HasForeignKey(d => d.OwnerEmail)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__propertyF__Owner__300424B4");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Email);

            entity.ToTable("user");

            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.Phone)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("phone");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

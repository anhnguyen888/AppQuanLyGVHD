using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MyAspNetCoreApp.Models;

public partial class ThesisManagementDbContext : DbContext
{
    public ThesisManagementDbContext()
    {
    }

    public ThesisManagementDbContext(DbContextOptions<ThesisManagementDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<GiangVien> GiangViens { get; set; }

    public virtual DbSet<SinhVien> SinhViens { get; set; }

    public virtual DbSet<SinhVienGiangVienHuongDan> SinhVienGiangVienHuongDans { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=ThesisManagementDB;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<GiangVien>(entity =>
        {
            entity.HasKey(e => e.GiangVienId).HasName("PK__GiangVie__626127822D0795EA");

            entity.ToTable("GiangVien");

            entity.HasIndex(e => e.Email, "UQ__GiangVie__A9D10534D16797E5").IsUnique();

            entity.HasIndex(e => e.MaGiangVien, "UQ__GiangVie__C03BEEBB302510F4").IsUnique();

            entity.Property(e => e.GiangVienId).HasColumnName("GiangVienID");
            entity.Property(e => e.BoMon).HasMaxLength(100);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.HoTen).HasMaxLength(100);
            entity.Property(e => e.MaGiangVien).HasMaxLength(20);
            entity.Property(e => e.SoDienThoai).HasMaxLength(15);
        });

        modelBuilder.Entity<SinhVien>(entity =>
        {
            entity.HasKey(e => e.SinhVienId).HasName("PK__SinhVien__F3CF812E24C70A1E");

            entity.ToTable("SinhVien");

            entity.HasIndex(e => e.MaSinhVien, "UQ__SinhVien__939AE774DB11B452").IsUnique();

            entity.Property(e => e.SinhVienId).HasColumnName("SinhVienID");
            entity.Property(e => e.HoTen).HasMaxLength(100);
            entity.Property(e => e.Lop).HasMaxLength(50);
            entity.Property(e => e.MaSinhVien).HasMaxLength(20);
        });

        modelBuilder.Entity<SinhVienGiangVienHuongDan>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__SinhVien__3214EC273D56E2B6");

            entity.ToTable("SinhVienGiangVienHuongDan");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.GhiChu).HasMaxLength(255);
            entity.Property(e => e.GiangVienId).HasColumnName("GiangVienID");
            entity.Property(e => e.NgayBatDau).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.SinhVienId).HasColumnName("SinhVienID");

            entity.HasOne(d => d.GiangVien).WithMany(p => p.SinhVienGiangVienHuongDans)
                .HasForeignKey(d => d.GiangVienId)
                .HasConstraintName("FK__SinhVienG__Giang__3F466844");

            entity.HasOne(d => d.SinhVien).WithMany(p => p.SinhVienGiangVienHuongDans)
                .HasForeignKey(d => d.SinhVienId)
                .HasConstraintName("FK__SinhVienG__SinhV__3E52440B");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.PasswordHash).HasMaxLength(255);
            entity.Property(e => e.Role).HasMaxLength(50);
            entity.Property(e => e.Username).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

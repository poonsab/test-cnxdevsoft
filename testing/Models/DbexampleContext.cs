using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace testing.AddModels;

public partial class DbexampleContext : DbContext
{
    public DbexampleContext()
    {
    }

    public DbexampleContext(DbContextOptions<DbexampleContext> options)
        : base(options)
    {
    }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserType> UserTypes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=POONSAB-E14\\SQLEXPRESS;Database=DBExample;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.Active).HasDefaultValueSql("((1))");
            entity.Property(e => e.Fname)
                .HasMaxLength(100)
                .HasColumnName("FName");
            entity.Property(e => e.Lname)
                .HasMaxLength(100)
                .HasColumnName("LName");
            entity.Property(e => e.UserTypeId).HasDefaultValueSql("((2))");

            entity.HasOne(d => d.UserType).WithMany(p => p.Users)
                .HasForeignKey(d => d.UserTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Users_UserType");
        });

        modelBuilder.Entity<UserType>(entity =>
        {
            entity.ToTable("UserType");

            entity.Property(e => e.Active).HasDefaultValueSql("((1))");
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

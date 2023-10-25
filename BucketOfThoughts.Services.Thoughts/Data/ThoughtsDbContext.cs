using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BucketOfThoughts.Services.Thoughts.Data;

public partial class ThoughtsDbContext : DbContext
{
    public ThoughtsDbContext()
    {
    }

    public ThoughtsDbContext(DbContextOptions<ThoughtsDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Thought> Thoughts { get; set; }

    public virtual DbSet<ThoughtCategory> ThoughtCategories { get; set; }

    public virtual DbSet<ThoughtDetail> ThoughtDetails { get; set; }

    public virtual DbSet<ThoughtModule> ThoughtModules { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Thought>(entity =>
        {
            entity.HasKey(e => e.ThoughtId).HasName("PK__Thought__0945E46BF9CB9C2F");

            entity.ToTable("Thought");

            entity.Property(e => e.RecordDateTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ThoughtGuid).HasDefaultValueSql("(newid())");

            entity.HasOne(d => d.ThoughtCategory).WithMany(p => p.Thoughts)
                .HasForeignKey(d => d.ThoughtCategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Thought_ThoughtCategory");
        });

        modelBuilder.Entity<ThoughtCategory>(entity =>
        {
            entity.HasKey(e => e.ThoughtCategoryId).HasName("PK__ThoughtC__B82763E00CD22898");

            entity.ToTable("ThoughtCategory");

            entity.Property(e => e.RecordDateTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ThoughtCategoryGuid).HasDefaultValueSql("(newid())");

            entity.HasOne(d => d.ThoughtModule).WithMany(p => p.ThoughtCategories)
                .HasForeignKey(d => d.ThoughtModuleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ThoughtCategory_ThoughtModule");
        });

        modelBuilder.Entity<ThoughtDetail>(entity =>
        {
            entity.HasKey(e => e.ThoughtDetailId).HasName("PK__ThoughtD__F3E2F8286C662AA8");

            entity.ToTable("ThoughtDetail");

            entity.Property(e => e.RecordDateTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ThoughtDetailGuid).HasDefaultValueSql("(newid())");

            entity.HasOne(d => d.Thought).WithMany(p => p.ThoughtDetails)
                .HasForeignKey(d => d.ThoughtId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ThoughtDetail_Thought");
        });

        modelBuilder.Entity<ThoughtModule>(entity =>
        {
            entity.HasKey(e => e.ThoughtModuleId).HasName("PK__ThoughtM__38FE9FD688EF962A");

            entity.ToTable("ThoughtModule");

            entity.Property(e => e.RecordDateTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ThoughtModuleGuid).HasDefaultValueSql("(newid())");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

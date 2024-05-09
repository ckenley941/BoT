﻿using BucketOfThoughts.Core.Infrastructure.BaseClasses;
using Microsoft.EntityFrameworkCore;

namespace BucketOfThoughts.Services.Thoughts.Data;

public partial class ThoughtsDbContext : BaseDbContext<ThoughtsDbContext>
{
    public ThoughtsDbContext()
    {
    }

    public ThoughtsDbContext(DbContextOptions<ThoughtsDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<RelatedThought> RelatedThoughts { get; set; }

    public virtual DbSet<Thought> Thoughts { get; set; }

    public virtual DbSet<ThoughtCategory> ThoughtCategories { get; set; }

    public virtual DbSet<ThoughtDetail> ThoughtDetails { get; set; }

    public virtual DbSet<ThoughtModule> ThoughtModules { get; set; }

    public virtual DbSet<ThoughtWebsiteLink> ThoughtWebsiteLinks { get; set; }

    public virtual DbSet<WebsiteLink> WebsiteLinks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<RelatedThought>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.ToTable("RelatedThought");

            entity.Property(e => e.CreatedDateTime)
                .HasDefaultValueSql("(getutcdate())");

            entity.HasOne(d => d.ThoughtId1Navigation).WithMany(p => p.RelatedThoughtThoughtId1Navigations)
                .HasForeignKey(d => d.ThoughtId1)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RelatedThought_Thought1");

            entity.HasOne(d => d.ThoughtId2Navigation).WithMany(p => p.RelatedThoughtThoughtId2Navigations)
                .HasForeignKey(d => d.ThoughtId2)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RelatedThought_Thought2");
        });

        modelBuilder.Entity<Thought>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.ToTable("Thought");

            entity.Property(e => e.CreatedDateTime)
                .HasDefaultValueSql("(getutcdate())");

            entity.Property(e => e.ThoughtGuid)
                .HasDefaultValueSql("(newid())");

            entity.HasOne(d => d.ThoughtCategory).WithMany(p => p.Thoughts)
                .HasForeignKey(d => d.ThoughtCategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Thought_ThoughtCategory");
        });

        modelBuilder.Entity<ThoughtCategory>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.ToTable("ThoughtCategory");

            entity.Property(e => e.CreatedDateTime)
                .HasDefaultValueSql("(getutcdate())");

            entity.HasOne(d => d.ThoughtModule).WithMany(p => p.ThoughtCategories)
                .HasForeignKey(d => d.ThoughtModuleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ThoughtCategory_ThoughtModule");
        });

        modelBuilder.Entity<ThoughtDetail>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.ToTable("ThoughtDetail");

            entity.Property(e => e.CreatedDateTime)
                .HasDefaultValueSql("(getutcdate())");

            entity.HasOne(d => d.Thought).WithMany(p => p.ThoughtDetails)
                .HasForeignKey(d => d.ThoughtId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ThoughtDetail_Thought");
        });

        modelBuilder.Entity<ThoughtModule>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.ToTable("ThoughtModule");

            entity.Property(e => e.CreatedDateTime)
                .HasDefaultValueSql("(getutcdate())");
        });

        modelBuilder.Entity<ThoughtWebsiteLink>(entity =>
        {
            entity.HasKey(e => new { e.ThoughtId, e.WebsiteLinkId });

            entity.ToTable("ThoughtWebsiteLink");

            entity.HasOne(d => d.Thought).WithMany(p => p.ThoughtWebsiteLinks)
                .HasForeignKey(d => d.ThoughtId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ThoughtWebsiteLink_Thought");

            entity.HasOne(d => d.WebsiteLink).WithMany(p => p.ThoughtWebsiteLinks)
                .HasForeignKey(d => d.WebsiteLinkId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ThoughtWebsiteLink_WebsiteLink");
        });

        modelBuilder.Entity<WebsiteLink>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.ToTable("WebsiteLink");

            entity.Property(e => e.CreatedDateTime)
                .HasDefaultValueSql("(getutcdate())");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

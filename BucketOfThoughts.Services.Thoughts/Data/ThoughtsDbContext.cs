using BucketOfThoughts.Core.Infrastructure.BaseClasses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

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
    public virtual DbSet<OutdoorActivityLog> OutdoorActivityLogs { get; set; }

    public virtual DbSet<RelatedThought> RelatedThoughts { get; set; }

    public virtual DbSet<Thought> Thoughts { get; set; }

    public virtual DbSet<ThoughtBucket> ThoughtBuckets { get; set; }

    public virtual DbSet<ThoughtDetail> ThoughtDetails { get; set; }

    public virtual DbSet<ThoughtModule> ThoughtModules { get; set; }

    public virtual DbSet<ThoughtWebsiteLink> ThoughtWebsiteLinks { get; set; }

    public virtual DbSet<WebsiteLink> WebsiteLinks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<OutdoorActivityLog>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.ToTable("OutdoorActivityLog");

            entity.Property(e => e.ActivityName)
               .HasMaxLength(250);

            entity.Property(e => e.ActivityType)
               .HasMaxLength(100);

            entity.Property(e => e.CreatedDateTime)
                .HasDefaultValueSql("(getutcdate())");
        });

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

            entity.Property(e => e.TextType)
               .HasDefaultValueSql("'PlainText'")
               .HasMaxLength(25);

            entity.HasOne(d => d.ThoughtBucket).WithMany(p => p.Thoughts)
                .HasForeignKey(d => d.ThoughtBucketId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Thought_ThoughtBucket");
        });

        modelBuilder.Entity<ThoughtBucket>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.ToTable("ThoughtBucket");

            entity.Property(e => e.CreatedDateTime)
                .HasDefaultValueSql("(getutcdate())");

            entity.Property(e => e.ShowOnDashboard)
               .HasDefaultValue(true);


            entity.HasOne(d => d.ThoughtModule).WithMany(p => p.ThoughtBuckets)
                .HasForeignKey(d => d.ThoughtModuleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ThoughtBucket_ThoughtModule");
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

    protected override void ConfigureConventions(ModelConfigurationBuilder builder)
    {
        builder.Properties<DateOnly>()
               .HaveConversion<DateOnlyConverter>()
               .HaveColumnType("date");
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

/// <summary>
/// Converts <see cref="DateOnly" /> to <see cref="DateTime"/> and vice versa.
/// </summary>
public class DateOnlyConverter : ValueConverter<DateOnly, DateTime>
{
    /// <summary>
    /// Creates a new instance of this converter.
    /// </summary>
    public DateOnlyConverter() : base(
        d => d.ToDateTime(TimeOnly.MinValue),
        d => DateOnly.FromDateTime(d))
    { }
}

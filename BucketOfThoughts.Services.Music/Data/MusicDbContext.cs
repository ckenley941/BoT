using BucketOfThoughts.Core.Infrastructure.BaseClasses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BucketOfThoughts.Services.Music.Data;

public partial class MusicDbContext : BaseDbContext<MusicDbContext>
{
    public MusicDbContext()
    {
    }

    public MusicDbContext(DbContextOptions<MusicDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Band> Bands { get; set; }
    public virtual DbSet<Concert> Concerts { get; set; }
    public virtual DbSet<SetlistSong> SetlistSongs { get; set; }
    public virtual DbSet<Venue> Venues { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Band>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.ToTable("Band");

            entity.Property(e => e.CreatedDateTime).HasDefaultValueSql("(getutcdate())");
        });

        modelBuilder.Entity<Concert>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.ToTable("Concert");

            entity.Property(e => e.CreatedDateTime).HasDefaultValueSql("(getutcdate())");

            entity.HasOne(d => d.Band).WithMany(p => p.Concerts)
              .HasForeignKey(d => d.BandId)
              .OnDelete(DeleteBehavior.ClientSetNull)
              .HasConstraintName("FK_Concert_Band");

            entity.HasOne(d => d.Venue).WithMany(p => p.Concerts)
             .HasForeignKey(d => d.VenueId)
             .OnDelete(DeleteBehavior.ClientSetNull)
             .HasConstraintName("FK_Concert_Venue");
        });

        modelBuilder.Entity<SetlistSong>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.ToTable("SetlistSong");

            entity.Property(e => e.SetNo)
               .HasMaxLength(5);

            entity.Property(e => e.CreatedDateTime).HasDefaultValueSql("(getutcdate())");

            entity.HasOne(d => d.Concert).WithMany(p => p.Songs)
            .HasForeignKey(d => d.ConcertId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_SetlistSong_Concert");
        });

        modelBuilder.Entity<Venue>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.ToTable("Venue");

            entity.Property(e => e.CreatedDateTime).HasDefaultValueSql("(getutcdate())");
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


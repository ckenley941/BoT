using BucketOfThoughts.Core.Infrastructure.BaseClasses;
using Microsoft.EntityFrameworkCore;

namespace BucketOfThoughts.Services.Shared.Data;

public partial class SharedDbContext : BaseDbContext<SharedDbContext>
{
    public SharedDbContext()
    {
    }

    public SharedDbContext(DbContextOptions<SharedDbContext> options)
        : base(options)
    {
    }
    public virtual DbSet<Note> Notes { get; set; }
    public virtual DbSet<WebsiteLink> WebsiteLinks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Note>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.ToTable("Note");

            entity.Property(e => e.CreatedDateTime)
                .HasDefaultValueSql("(getutcdate())");
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

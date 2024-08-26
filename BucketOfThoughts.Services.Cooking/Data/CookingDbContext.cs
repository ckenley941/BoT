using BucketOfThoughts.Core.Infrastructure.BaseClasses;
using BucketOfThoughts.Services.Cooking.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BucketOfThoughts.Services.Thoughts.Data;

public partial class CookingDbContext : BaseDbContext<CookingDbContext>
{
    public CookingDbContext()
    {
    }

    public CookingDbContext(DbContextOptions<CookingDbContext> options)
        : base(options)
    {
    }
    //public virtual DbSet<Note> Notes { get; set; }
    public virtual DbSet<Recipe> Recipes { get; set; }
    public virtual DbSet<RecipeIngredient> RecipeIngredients { get; set; }
    public virtual DbSet<RecipeInstruction> RecipeInstructions { get; set; }
    public virtual DbSet<RecipeNote> RecipeNotes { get; set; }
    public virtual DbSet<RecipeWebsiteLink> RecipeWebsiteLinks { get; set; }
   // public virtual DbSet<WebsiteLink> WebsiteLinks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Recipe>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.ToTable("Recipe");

            entity.Property(e => e.Name)
               .HasMaxLength(250);

            entity.Property(e => e.Protein)
             .HasMaxLength(100);

            entity.Property(e => e.Category)
             .HasMaxLength(250);

            entity.Property(e => e.CuisineType)
             .HasMaxLength(100);

            entity.Property(e => e.PrepTime)
             .HasMaxLength(50);

            entity.Property(e => e.CookTime)
             .HasMaxLength(50);

            entity.Property(e => e.TotalTime)
             .HasMaxLength(50);

            entity.Property(e => e.ServeWith)
           .HasMaxLength(250);

            entity.Property(e => e.CreatedDateTime)
                .HasDefaultValueSql("(getutcdate())");
        });

        modelBuilder.Entity<RecipeIngredient>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.ToTable("RecipeIngredient");

            entity.Property(e => e.Measurement)
               .HasMaxLength(50);

            entity.Property(e => e.CreatedDateTime)
                .HasDefaultValueSql("(getutcdate())");

            entity.HasOne(d => d.Recipe).WithMany(p => p.RecipeIngredients)
               .HasForeignKey(d => d.RecipeId)
               .OnDelete(DeleteBehavior.ClientSetNull)
               .HasConstraintName("FK_RecipeIngredient_Recipe");
        });

        modelBuilder.Entity<RecipeInstruction>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.ToTable("RecipeInstruction");

            entity.Property(e => e.CreatedDateTime)
                .HasDefaultValueSql("(getutcdate())");

            entity.HasOne(d => d.Recipe).WithMany(p => p.RecipeInstructons)
              .HasForeignKey(d => d.RecipeId)
              .OnDelete(DeleteBehavior.ClientSetNull)
              .HasConstraintName("FK_RecipeInstruction_Recipe");
        });

        modelBuilder.Entity<RecipeNote>(entity =>
        {
            entity.HasKey(e => new { e.RecipeId, e.NoteId });

            entity.ToTable("RecipeNote");

            entity.HasOne(d => d.Recipe).WithMany(p => p.RecipeNotes)
                .HasForeignKey(d => d.RecipeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Note_Recipe");

            //entity.HasOne(d => d.WebsiteLink).WithMany(p => p.RecipeWebsiteLinks)
            //    .HasForeignKey(d => d.WebsiteLinkId)
            //    .OnDelete(DeleteBehavior.ClientSetNull)
            //    .HasConstraintName("FK_RecipeWebsiteLink_WebsiteLink");
        });

        modelBuilder.Entity<RecipeWebsiteLink>(entity =>
        {
            entity.HasKey(e => new { e.RecipeId, e.WebsiteLinkId });

            entity.ToTable("RecipeWebsiteLink");

            entity.HasOne(d => d.Recipe).WithMany(p => p.RecipeWebsiteLinks)
                .HasForeignKey(d => d.RecipeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RecipeWebsiteLink_Recipe");

            //entity.HasOne(d => d.WebsiteLink).WithMany(p => p.RecipeWebsiteLinks)
            //    .HasForeignKey(d => d.WebsiteLinkId)
            //    .OnDelete(DeleteBehavior.ClientSetNull)
            //    .HasConstraintName("FK_RecipeWebsiteLink_WebsiteLink");
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

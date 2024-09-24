using Microsoft.EntityFrameworkCore;

namespace BucketOfThoughts.Services.Languages.Data;

public partial class LanguageDbContext : DbContext
{
    public LanguageDbContext()
    {
    }

    public LanguageDbContext(DbContextOptions<LanguageDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Word> Words { get; set; }

    public virtual DbSet<WordContext> WordContexts { get; set; }

    public virtual DbSet<WordExample> WordExamples { get; set; }

    public virtual DbSet<WordPronunciation> WordPronunciations { get; set; }

    public virtual DbSet<WordRelationship> WordRelationships { get; set; }

    public virtual DbSet<WordXref> WordXrefs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Word>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.ToTable("Word");

            entity.HasIndex(e => e.LanguageId, "IDX_Language");

            entity.Property(e => e.CreatedDateTime).HasDefaultValueSql("(getutcdate())");
        });

        modelBuilder.Entity<WordContext>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.ToTable("WordContext");

            entity.Property(e => e.CreatedDateTime).HasDefaultValueSql("(getutcdate())");

            entity.HasOne(d => d.WordXref).WithMany(p => p.WordContexts)
                .HasForeignKey(d => d.WordXrefId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_WordContext_WordXref");
        });

        modelBuilder.Entity<WordExample>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.ToTable("WordExample");

            entity.Property(e => e.CreatedDateTime).HasDefaultValueSql("(getutcdate())");

            entity.HasOne(d => d.WordContext).WithMany(p => p.WordExamples)
                .HasForeignKey(d => d.WordContextId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_WordExample_WordContext");
        });

        modelBuilder.Entity<WordFlashCardSet>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.ToTable("WordFlashCardSet");

            entity.Property(e => e.CreatedDateTime).HasDefaultValueSql("(getutcdate())");
        });

        modelBuilder.Entity<WordFlashCardSetDetail>(entity =>
        {
            entity.HasKey(e => new { e.WordXrefId, e.WordFlashCardSetId });

            entity.ToTable("WordFlashCardSetDetail");

            entity.HasOne(d => d.WordXref).WithMany(p => p.WordFlashCardSetDetails)
                .HasForeignKey(d => d.WordXrefId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_WordFlashCardSetDetail_WordXref");

            entity.HasOne(d => d.WordFlashCardSet).WithMany(p => p.Details)
                .HasForeignKey(d => d.WordFlashCardSetId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_WordFlashCardSetDetail_WordFlashCardSet");
        });

        modelBuilder.Entity<WordPronunciation>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.ToTable("WordPronunciation");

            entity.Property(e => e.CreatedDateTime).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.Phonetic).HasMaxLength(100);

            entity.HasOne(d => d.Word).WithMany(p => p.WordPronunciations)
                .HasForeignKey(d => d.WordId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_WordPronunciation_Word");
        });

        modelBuilder.Entity<WordRelationship>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.ToTable("WordRelationship");

            entity.Property(e => e.CreatedDateTime).HasDefaultValueSql("(getutcdate())");

            entity.HasOne(d => d.WordId1Navigation).WithMany(p => p.WordRelationshipWordId1Navigations)
                .HasForeignKey(d => d.WordId1)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_WordRelationship_Word1");

            entity.HasOne(d => d.WordId2Navigation).WithMany(p => p.WordRelationshipWordId2Navigations)
                .HasForeignKey(d => d.WordId2)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_WordRelationship_Word2");
        });

        modelBuilder.Entity<WordXref>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.ToTable("WordXref");

            entity.Property(e => e.CreatedDateTime).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.IsPrimaryTranslation).HasDefaultValueSql("((0))");

            entity.HasOne(d => d.WordId1Navigation).WithMany(p => p.WordXrefWordId1Navigations)
                .HasForeignKey(d => d.WordId1)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_WordXref_Word1");

            entity.HasOne(d => d.WordId2Navigation).WithMany(p => p.WordXrefWordId2Navigations)
                .HasForeignKey(d => d.WordId2)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_WordXref_Word2");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

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
            entity.HasKey(e => e.WordId).HasName("PK__Word__3D7832E5997AB028");

            entity.ToTable("Word");

            entity.HasIndex(e => e.LanguageId, "IDX_Language");

            entity.Property(e => e.CreatedDateTime).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.RecordDateTime).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.WordGuid).HasDefaultValueSql("(newid())");
        });

        modelBuilder.Entity<WordContext>(entity =>
        {
            entity.HasKey(e => e.WordContextId).HasName("PK__WordCont__ADD52A42F2C4E002");

            entity.ToTable("WordContext");

            entity.Property(e => e.CreatedDateTime).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.RecordDateTime).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.WordContextGuid).HasDefaultValueSql("(newid())");

            entity.HasOne(d => d.WordXref).WithMany(p => p.WordContexts)
                .HasForeignKey(d => d.WordXrefId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_WordContext_WordXref");
        });

        modelBuilder.Entity<WordExample>(entity =>
        {
            entity.HasKey(e => e.WordExampleId).HasName("PK__WordExam__56A9A3F923BA9246");

            entity.ToTable("WordExample");

            entity.Property(e => e.CreatedDateTime).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.RecordDateTime).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.WordExampleGuid).HasDefaultValueSql("(newid())");

            entity.HasOne(d => d.WordContext).WithMany(p => p.WordExamples)
                .HasForeignKey(d => d.WordContextId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_WordExample_WordContext");
        });

        modelBuilder.Entity<WordPronunciation>(entity =>
        {
            entity.HasKey(e => e.WordPronunciationId).HasName("PK__WordPron__644F09DFF5A54F85");

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
            entity.HasKey(e => e.WordRelationshipId).HasName("PK__WordRelationship__19EF1379B03C8E1C");

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
            entity.HasKey(e => e.WordXrefId).HasName("PK__WordXref__19EF1379B03C8E1C");

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

using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Thoughts.Data.SqlServer;

public partial class BucketOfThoughtsProdContext : DbContext
{
    public BucketOfThoughtsProdContext()
    {
    }

    public BucketOfThoughtsProdContext(DbContextOptions<BucketOfThoughtsProdContext> options)
        : base(options)
    {
    }

    public virtual DbSet<DataType> DataTypes { get; set; }

    public virtual DbSet<Thought> Thoughts { get; set; }

    public virtual DbSet<ThoughtAdditionalInfo> ThoughtAdditionalInfos { get; set; }

    public virtual DbSet<ThoughtCategory> ThoughtCategories { get; set; }

    public virtual DbSet<ThoughtCategoryAdditionalInfo> ThoughtCategoryAdditionalInfos { get; set; }

    public virtual DbSet<ThoughtDetail> ThoughtDetails { get; set; }

    public virtual DbSet<ThoughtModule> ThoughtModules { get; set; }

    public virtual DbSet<ThoughtTimeline> ThoughtTimelines { get; set; }

    public virtual DbSet<ThoughtWebsiteLink> ThoughtWebsiteLinks { get; set; }

    public virtual DbSet<Timeline> Timelines { get; set; }

    public virtual DbSet<VwThoughtDetail> VwThoughtDetails { get; set; }

    public virtual DbSet<WebsiteLink> WebsiteLinks { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.\\SQLExpress;Database=BucketOfThoughts_Prod;Trusted_Connection=True;TrustServerCertificate=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DataType>(entity =>
        {
            entity.HasKey(e => e.DataTypeId).HasName("PK__DataType__4382081F8996DCAC");

            entity.ToTable("DataType");
        });

        modelBuilder.Entity<Thought>(entity =>
        {
            entity.HasKey(e => e.ThoughtId).HasName("PK__Thought__0945E46B03429586");

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

        modelBuilder.Entity<ThoughtAdditionalInfo>(entity =>
        {
            entity.HasKey(e => e.ThoughtAdditionalInfoId).HasName("PK__ThoughtA__C47EEE2F2A398DE0");

            entity.ToTable("ThoughtAdditionalInfo");

            entity.HasOne(d => d.ThoughtCategoryAdditionalInfo).WithMany(p => p.ThoughtAdditionalInfos)
                .HasForeignKey(d => d.ThoughtCategoryAdditionalInfoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ThoughtAdditionalInfo_ThoughtCategoryAdditionalInfo");

            entity.HasOne(d => d.ThoughtDetail).WithMany(p => p.ThoughtAdditionalInfos)
                .HasForeignKey(d => d.ThoughtDetailId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ThoughtAdditionalInfo_ThoughtDetail");
        });

        modelBuilder.Entity<ThoughtCategory>(entity =>
        {
            entity.HasKey(e => e.ThoughtCategoryId).HasName("PK__ThoughtC__B82763E01C7DA040");

            entity.ToTable("ThoughtCategory");

            entity.Property(e => e.RecordDateTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ThoughtCategoryGuid).HasDefaultValueSql("(newid())");

            entity.HasOne(d => d.ThoughtModule).WithMany(p => p.ThoughtCategories)
                .HasForeignKey(d => d.ThoughtModuleId)
                .HasConstraintName("FK_ThoughtCategory_ThoughtModule");
        });

        modelBuilder.Entity<ThoughtCategoryAdditionalInfo>(entity =>
        {
            entity.HasKey(e => e.ThoughtCategoryAdditionalInfoId).HasName("PK__ThoughtC__9EF701B44C96FAB1");

            entity.ToTable("ThoughtCategoryAdditionalInfo");

            entity.Property(e => e.RecordDateTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ThoughtCategoryAdditionalInfoGuid).HasDefaultValueSql("(newid())");

            entity.HasOne(d => d.DataType).WithMany(p => p.ThoughtCategoryAdditionalInfos)
                .HasForeignKey(d => d.DataTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ThoughtCategoryAdditionalInfo_DataType");

            entity.HasOne(d => d.ThoughtCategory).WithMany(p => p.ThoughtCategoryAdditionalInfos)
                .HasForeignKey(d => d.ThoughtCategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ThoughtCategoryAdditionalInfo_ThoughtCategory");
        });

        modelBuilder.Entity<ThoughtDetail>(entity =>
        {
            entity.HasKey(e => e.ThoughtDetailId).HasName("PK__ThoughtD__F3E2F82897EC58DE");

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
            entity.HasKey(e => e.ThoughtModuleId).HasName("PK__ThoughtM__38FE9FD6E6AC74AB");

            entity.ToTable("ThoughtModule");

            entity.Property(e => e.RecordDateTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ThoughtModuleGuid).HasDefaultValueSql("(newid())");
        });

        modelBuilder.Entity<ThoughtTimeline>(entity =>
        {
            entity.HasKey(e => e.ThoughtTimelineId).HasName("PK__ThoughtT__7F596EA63EFA1A60");

            entity.ToTable("ThoughtTimeline");

            entity.HasOne(d => d.Thought).WithMany(p => p.ThoughtTimelines)
                .HasForeignKey(d => d.ThoughtId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ThoughtTimeline_Thought");

            entity.HasOne(d => d.Timeline).WithMany(p => p.ThoughtTimelines)
                .HasForeignKey(d => d.TimelineId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ThoughtTimeline_Timeline");
        });

        modelBuilder.Entity<ThoughtWebsiteLink>(entity =>
        {
            entity.HasKey(e => e.ThoughtWebsiteLinkId).HasName("PK__ThoughtW__017013A4F6D0563D");

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

        modelBuilder.Entity<Timeline>(entity =>
        {
            entity.HasKey(e => e.TimelineId).HasName("PK__Timeline__1DE4F085CCCE7944");

            entity.ToTable("Timeline");

            entity.Property(e => e.DateEnd).HasColumnType("datetime");
            entity.Property(e => e.DateStart).HasColumnType("datetime");
            entity.Property(e => e.RecordDateTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.TimelineGuid).HasDefaultValueSql("(newid())");
        });

        modelBuilder.Entity<VwThoughtDetail>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vwThoughtDetails");

            entity.Property(e => e.DateEnd).HasColumnType("datetime");
            entity.Property(e => e.DateStart).HasColumnType("datetime");
        });

        modelBuilder.Entity<WebsiteLink>(entity =>
        {
            entity.HasKey(e => e.WebsiteLinkId).HasName("PK__WebsiteL__FE250C559E71FD6D");

            entity.ToTable("WebsiteLink");

            entity.Property(e => e.RecordDateTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.WebsiteLinkGuid).HasDefaultValueSql("(newid())");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

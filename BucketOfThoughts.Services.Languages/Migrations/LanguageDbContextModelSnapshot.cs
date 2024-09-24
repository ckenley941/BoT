﻿// <auto-generated />
using System;
using BucketOfThoughts.Services.Languages.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BucketOfThoughts.Services.Languages.Migrations
{
    [DbContext(typeof(LanguageDbContext))]
    partial class LanguageDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BucketOfThoughts.Services.Languages.Data.Word", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnOrder(1);

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTimeOffset>("CreatedDateTime")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetimeoffset")
                        .HasColumnOrder(2)
                        .HasDefaultValueSql("(getutcdate())");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("LanguageId")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset?>("ModifiedDateTime")
                        .HasColumnType("datetimeoffset")
                        .HasColumnOrder(3);

                    b.HasKey("Id");

                    b.HasIndex(new[] { "LanguageId" }, "IDX_Language");

                    b.ToTable("Word", (string)null);
                });

            modelBuilder.Entity("BucketOfThoughts.Services.Languages.Data.WordContext", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnOrder(1);

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ContextDesc")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("CreatedDateTime")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetimeoffset")
                        .HasColumnOrder(2)
                        .HasDefaultValueSql("(getutcdate())");

                    b.Property<DateTimeOffset?>("ModifiedDateTime")
                        .HasColumnType("datetimeoffset")
                        .HasColumnOrder(3);

                    b.Property<int>("SortOrder")
                        .HasColumnType("int");

                    b.Property<int>("WordXrefId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("WordXrefId");

                    b.ToTable("WordContext", (string)null);
                });

            modelBuilder.Entity("BucketOfThoughts.Services.Languages.Data.WordExample", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnOrder(1);

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTimeOffset>("CreatedDateTime")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetimeoffset")
                        .HasColumnOrder(2)
                        .HasDefaultValueSql("(getutcdate())");

                    b.Property<DateTimeOffset?>("ModifiedDateTime")
                        .HasColumnType("datetimeoffset")
                        .HasColumnOrder(3);

                    b.Property<string>("Translation1")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Translation2")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("WordContextId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("WordContextId");

                    b.ToTable("WordExample", (string)null);
                });

            modelBuilder.Entity("BucketOfThoughts.Services.Languages.Data.WordFlashCardSet", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnOrder(1);

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTimeOffset>("CreatedDateTime")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetimeoffset")
                        .HasColumnOrder(2)
                        .HasDefaultValueSql("(getutcdate())");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset?>("ModifiedDateTime")
                        .HasColumnType("datetimeoffset")
                        .HasColumnOrder(3);

                    b.HasKey("Id");

                    b.ToTable("WordFlashCardSet", (string)null);
                });

            modelBuilder.Entity("BucketOfThoughts.Services.Languages.Data.WordFlashCardSetDetail", b =>
                {
                    b.Property<int>("WordXrefId")
                        .HasColumnType("int");

                    b.Property<int>("WordFlashCardSetId")
                        .HasColumnType("int");

                    b.HasKey("WordXrefId", "WordFlashCardSetId");

                    b.HasIndex("WordFlashCardSetId");

                    b.ToTable("WordFlashCardSetDetail", (string)null);
                });

            modelBuilder.Entity("BucketOfThoughts.Services.Languages.Data.WordPronunciation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnOrder(1);

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTimeOffset>("CreatedDateTime")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetimeoffset")
                        .HasColumnOrder(2)
                        .HasDefaultValueSql("(getutcdate())");

                    b.Property<string>("Phonetic")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("WordId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("WordId");

                    b.ToTable("WordPronunciation", (string)null);
                });

            modelBuilder.Entity("BucketOfThoughts.Services.Languages.Data.WordRelationship", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnOrder(1);

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTimeOffset>("CreatedDateTime")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetimeoffset")
                        .HasColumnOrder(2)
                        .HasDefaultValueSql("(getutcdate())");

                    b.Property<bool>("IsAntonym")
                        .HasColumnType("bit");

                    b.Property<bool>("IsPhrase")
                        .HasColumnType("bit");

                    b.Property<bool>("IsRelated")
                        .HasColumnType("bit");

                    b.Property<bool>("IsSynonym")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("ModifiedDateTime")
                        .HasColumnType("datetimeoffset")
                        .HasColumnOrder(3);

                    b.Property<int>("WordId1")
                        .HasColumnType("int");

                    b.Property<int>("WordId2")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("WordId1");

                    b.HasIndex("WordId2");

                    b.ToTable("WordRelationship", (string)null);
                });

            modelBuilder.Entity("BucketOfThoughts.Services.Languages.Data.WordXref", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnOrder(1);

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTimeOffset>("CreatedDateTime")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetimeoffset")
                        .HasColumnOrder(2)
                        .HasDefaultValueSql("(getutcdate())");

                    b.Property<bool?>("IsPrimaryTranslation")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValueSql("((0))");

                    b.Property<int>("SortOrder")
                        .HasColumnType("int");

                    b.Property<int>("WordId1")
                        .HasColumnType("int");

                    b.Property<int>("WordId2")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("WordId1");

                    b.HasIndex("WordId2");

                    b.ToTable("WordXref", (string)null);
                });

            modelBuilder.Entity("BucketOfThoughts.Services.Languages.Data.WordContext", b =>
                {
                    b.HasOne("BucketOfThoughts.Services.Languages.Data.WordXref", "WordXref")
                        .WithMany("WordContexts")
                        .HasForeignKey("WordXrefId")
                        .IsRequired()
                        .HasConstraintName("FK_WordContext_WordXref");

                    b.Navigation("WordXref");
                });

            modelBuilder.Entity("BucketOfThoughts.Services.Languages.Data.WordExample", b =>
                {
                    b.HasOne("BucketOfThoughts.Services.Languages.Data.WordContext", "WordContext")
                        .WithMany("WordExamples")
                        .HasForeignKey("WordContextId")
                        .IsRequired()
                        .HasConstraintName("FK_WordExample_WordContext");

                    b.Navigation("WordContext");
                });

            modelBuilder.Entity("BucketOfThoughts.Services.Languages.Data.WordFlashCardSetDetail", b =>
                {
                    b.HasOne("BucketOfThoughts.Services.Languages.Data.WordFlashCardSet", "WordFlashCardSet")
                        .WithMany("Details")
                        .HasForeignKey("WordFlashCardSetId")
                        .IsRequired()
                        .HasConstraintName("FK_WordFlashCardSetDetail_WordFlashCardSet");

                    b.HasOne("BucketOfThoughts.Services.Languages.Data.WordXref", "WordXref")
                        .WithMany("WordFlashCardSetDetails")
                        .HasForeignKey("WordXrefId")
                        .IsRequired()
                        .HasConstraintName("FK_WordFlashCardSetDetail_WordXref");

                    b.Navigation("WordFlashCardSet");

                    b.Navigation("WordXref");
                });

            modelBuilder.Entity("BucketOfThoughts.Services.Languages.Data.WordPronunciation", b =>
                {
                    b.HasOne("BucketOfThoughts.Services.Languages.Data.Word", "Word")
                        .WithMany("WordPronunciations")
                        .HasForeignKey("WordId")
                        .IsRequired()
                        .HasConstraintName("FK_WordPronunciation_Word");

                    b.Navigation("Word");
                });

            modelBuilder.Entity("BucketOfThoughts.Services.Languages.Data.WordRelationship", b =>
                {
                    b.HasOne("BucketOfThoughts.Services.Languages.Data.Word", "WordId1Navigation")
                        .WithMany("WordRelationshipWordId1Navigations")
                        .HasForeignKey("WordId1")
                        .IsRequired()
                        .HasConstraintName("FK_WordRelationship_Word1");

                    b.HasOne("BucketOfThoughts.Services.Languages.Data.Word", "WordId2Navigation")
                        .WithMany("WordRelationshipWordId2Navigations")
                        .HasForeignKey("WordId2")
                        .IsRequired()
                        .HasConstraintName("FK_WordRelationship_Word2");

                    b.Navigation("WordId1Navigation");

                    b.Navigation("WordId2Navigation");
                });

            modelBuilder.Entity("BucketOfThoughts.Services.Languages.Data.WordXref", b =>
                {
                    b.HasOne("BucketOfThoughts.Services.Languages.Data.Word", "WordId1Navigation")
                        .WithMany("WordXrefWordId1Navigations")
                        .HasForeignKey("WordId1")
                        .IsRequired()
                        .HasConstraintName("FK_WordXref_Word1");

                    b.HasOne("BucketOfThoughts.Services.Languages.Data.Word", "WordId2Navigation")
                        .WithMany("WordXrefWordId2Navigations")
                        .HasForeignKey("WordId2")
                        .IsRequired()
                        .HasConstraintName("FK_WordXref_Word2");

                    b.Navigation("WordId1Navigation");

                    b.Navigation("WordId2Navigation");
                });

            modelBuilder.Entity("BucketOfThoughts.Services.Languages.Data.Word", b =>
                {
                    b.Navigation("WordPronunciations");

                    b.Navigation("WordRelationshipWordId1Navigations");

                    b.Navigation("WordRelationshipWordId2Navigations");

                    b.Navigation("WordXrefWordId1Navigations");

                    b.Navigation("WordXrefWordId2Navigations");
                });

            modelBuilder.Entity("BucketOfThoughts.Services.Languages.Data.WordContext", b =>
                {
                    b.Navigation("WordExamples");
                });

            modelBuilder.Entity("BucketOfThoughts.Services.Languages.Data.WordFlashCardSet", b =>
                {
                    b.Navigation("Details");
                });

            modelBuilder.Entity("BucketOfThoughts.Services.Languages.Data.WordXref", b =>
                {
                    b.Navigation("WordContexts");

                    b.Navigation("WordFlashCardSetDetails");
                });
#pragma warning restore 612, 618
        }
    }
}

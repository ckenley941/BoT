﻿// <auto-generated />
using BucketOfThoughts.Services.Music.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BucketOfThoughts.Services.Music.Migrations
{
    [DbContext(typeof(MusicDbContext))]
    [Migration("20240627154425_AddMusicTables")]
    partial class AddMusicTables
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BucketOfThoughts.Services.Music.Data.Band", b =>
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
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("FormationYear")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset?>("ModifiedDateTime")
                        .HasColumnType("datetimeoffset")
                        .HasColumnOrder(3);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Origin")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Band", (string)null);
                });

            modelBuilder.Entity("BucketOfThoughts.Services.Music.Data.Concert", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnOrder(1);

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BandId")
                        .HasColumnType("int");

                    b.Property<DateTime>("ConcertDate")
                        .HasColumnType("date");

                    b.Property<string>("ConcertDayOfWeek")
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

                    b.Property<string>("Notes")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Story")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("VenueId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BandId");

                    b.HasIndex("VenueId");

                    b.ToTable("Concert", (string)null);
                });

            modelBuilder.Entity("BucketOfThoughts.Services.Music.Data.SetlistSong", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnOrder(1);

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ConcertId")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset>("CreatedDateTime")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetimeoffset")
                        .HasColumnOrder(2)
                        .HasDefaultValueSql("(getutcdate())");

                    b.Property<bool>("HasCarrot")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("ModifiedDateTime")
                        .HasColumnType("datetimeoffset")
                        .HasColumnOrder(3);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SetNo")
                        .HasColumnType("int");

                    b.Property<int?>("ShowGap")
                        .HasColumnType("int");

                    b.Property<DateTime?>("ShowGapLastPlayedDate")
                        .HasColumnType("datetime2");

                    b.Property<TimeSpan?>("SongLength")
                        .HasColumnType("time");

                    b.Property<int>("SongNo")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ConcertId");

                    b.ToTable("SetlistSong", (string)null);
                });

            modelBuilder.Entity("BucketOfThoughts.Services.Music.Data.Venue", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnOrder(1);

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("CreatedDateTime")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetimeoffset")
                        .HasColumnOrder(2)
                        .HasDefaultValueSql("(getutcdate())");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsFestival")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("ModifiedDateTime")
                        .HasColumnType("datetimeoffset")
                        .HasColumnOrder(3);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("State")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Venue", (string)null);
                });

            modelBuilder.Entity("BucketOfThoughts.Services.Music.Data.Concert", b =>
                {
                    b.HasOne("BucketOfThoughts.Services.Music.Data.Band", "Band")
                        .WithMany("Concerts")
                        .HasForeignKey("BandId")
                        .IsRequired()
                        .HasConstraintName("FK_Concert_Band");

                    b.HasOne("BucketOfThoughts.Services.Music.Data.Venue", "Venue")
                        .WithMany("Concerts")
                        .HasForeignKey("VenueId")
                        .HasConstraintName("FK_Concert_Venue");

                    b.Navigation("Band");

                    b.Navigation("Venue");
                });

            modelBuilder.Entity("BucketOfThoughts.Services.Music.Data.SetlistSong", b =>
                {
                    b.HasOne("BucketOfThoughts.Services.Music.Data.Concert", "Concert")
                        .WithMany("Songs")
                        .HasForeignKey("ConcertId")
                        .IsRequired()
                        .HasConstraintName("FK_SetlistSong_Concert");

                    b.Navigation("Concert");
                });

            modelBuilder.Entity("BucketOfThoughts.Services.Music.Data.Band", b =>
                {
                    b.Navigation("Concerts");
                });

            modelBuilder.Entity("BucketOfThoughts.Services.Music.Data.Concert", b =>
                {
                    b.Navigation("Songs");
                });

            modelBuilder.Entity("BucketOfThoughts.Services.Music.Data.Venue", b =>
                {
                    b.Navigation("Concerts");
                });
#pragma warning restore 612, 618
        }
    }
}

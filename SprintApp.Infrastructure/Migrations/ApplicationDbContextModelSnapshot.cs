﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SprintApp.Infrastructure.Data;

#nullable disable

namespace SprintApp.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.21")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("SprintApp.Core.Models.ProjectManager", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("EmailId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("LoginAtempt")
                        .HasColumnType("int");

                    b.Property<DateTime?>("LogoutTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("ManagerId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("ResetToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ResetTokenExpires")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("VarificationTokenExpires")
                        .HasColumnType("datetime2");

                    b.Property<string>("VerificationToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("VerifiedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("ProjectManagers");
                });

            modelBuilder.Entity("SprintApp.Core.Models.Sprint", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ManagerId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("ProjectManagerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("SprintId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ProjectManagerId");

                    b.ToTable("Sprints");
                });

            modelBuilder.Entity("SprintApp.Core.Models.UserStory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ManagerId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("ProjectManagerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("SprintId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("SprintId1")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("StoryId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ProjectManagerId");

                    b.HasIndex("SprintId1");

                    b.ToTable("UserStorys");
                });

            modelBuilder.Entity("SprintApp.Core.Models.Vote", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("SprintId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Value")
                        .HasColumnType("int");

                    b.Property<string>("VoterId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("VoterId1")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("SprintId");

                    b.HasIndex("VoterId1");

                    b.ToTable("Votes");
                });

            modelBuilder.Entity("SprintApp.Core.Models.Voter", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ManagerId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("ProjectManagerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("UserStoryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("VotedPoint")
                        .HasColumnType("int");

                    b.Property<string>("VoterId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ProjectManagerId");

                    b.HasIndex("UserStoryId");

                    b.ToTable("Voters");
                });

            modelBuilder.Entity("SprintApp.Core.Models.Sprint", b =>
                {
                    b.HasOne("SprintApp.Core.Models.ProjectManager", null)
                        .WithMany("Sprints")
                        .HasForeignKey("ProjectManagerId");
                });

            modelBuilder.Entity("SprintApp.Core.Models.UserStory", b =>
                {
                    b.HasOne("SprintApp.Core.Models.ProjectManager", null)
                        .WithMany("UserStories")
                        .HasForeignKey("ProjectManagerId");

                    b.HasOne("SprintApp.Core.Models.Sprint", null)
                        .WithMany("UserStories")
                        .HasForeignKey("SprintId1");
                });

            modelBuilder.Entity("SprintApp.Core.Models.Vote", b =>
                {
                    b.HasOne("SprintApp.Core.Models.Sprint", null)
                        .WithMany("Votes")
                        .HasForeignKey("SprintId");

                    b.HasOne("SprintApp.Core.Models.Voter", null)
                        .WithMany("Votes")
                        .HasForeignKey("VoterId1");
                });

            modelBuilder.Entity("SprintApp.Core.Models.Voter", b =>
                {
                    b.HasOne("SprintApp.Core.Models.ProjectManager", null)
                        .WithMany("Voters")
                        .HasForeignKey("ProjectManagerId");

                    b.HasOne("SprintApp.Core.Models.UserStory", null)
                        .WithMany("Voters")
                        .HasForeignKey("UserStoryId");
                });

            modelBuilder.Entity("SprintApp.Core.Models.ProjectManager", b =>
                {
                    b.Navigation("Sprints");

                    b.Navigation("UserStories");

                    b.Navigation("Voters");
                });

            modelBuilder.Entity("SprintApp.Core.Models.Sprint", b =>
                {
                    b.Navigation("UserStories");

                    b.Navigation("Votes");
                });

            modelBuilder.Entity("SprintApp.Core.Models.UserStory", b =>
                {
                    b.Navigation("Voters");
                });

            modelBuilder.Entity("SprintApp.Core.Models.Voter", b =>
                {
                    b.Navigation("Votes");
                });
#pragma warning restore 612, 618
        }
    }
}

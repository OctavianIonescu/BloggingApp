﻿// <auto-generated />
using System;
using BloggingApp.Web.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BloggingApp.Web.Migrations.BloggingAppDb
{
    [DbContext(typeof(BloggingAppDbContext))]
    [Migration("20230726024853_Initial Migration")]
    partial class InitialMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BlogPostTag", b =>
                {
                    b.Property<Guid>("blogPostsID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("tagsID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("blogPostsID", "tagsID");

                    b.HasIndex("tagsID");

                    b.ToTable("BlogPostTag");
                });

            modelBuilder.Entity("BloggingApp.Web.Models.Domain.BlogPost", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("URLHandle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("featuredURL")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("heading")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("pageTitle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("publishedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("shortDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("visible")
                        .HasColumnType("bit");

                    b.HasKey("ID");

                    b.ToTable("BlogPosts");
                });

            modelBuilder.Entity("BloggingApp.Web.Models.Domain.BlogPostComment", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("BlogPostID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DateAdded")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ID");

                    b.HasIndex("BlogPostID");

                    b.ToTable("BlogPostComment");
                });

            modelBuilder.Entity("BloggingApp.Web.Models.Domain.BlogPostLike", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("BlogPostID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ID");

                    b.HasIndex("BlogPostID");

                    b.ToTable("BlogPostLike");
                });

            modelBuilder.Entity("BloggingApp.Web.Models.Domain.Tag", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("displayName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("BlogPostTag", b =>
                {
                    b.HasOne("BloggingApp.Web.Models.Domain.BlogPost", null)
                        .WithMany()
                        .HasForeignKey("blogPostsID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BloggingApp.Web.Models.Domain.Tag", null)
                        .WithMany()
                        .HasForeignKey("tagsID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BloggingApp.Web.Models.Domain.BlogPostComment", b =>
                {
                    b.HasOne("BloggingApp.Web.Models.Domain.BlogPost", null)
                        .WithMany("BlogPostComments")
                        .HasForeignKey("BlogPostID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BloggingApp.Web.Models.Domain.BlogPostLike", b =>
                {
                    b.HasOne("BloggingApp.Web.Models.Domain.BlogPost", null)
                        .WithMany("BlogPostLikes")
                        .HasForeignKey("BlogPostID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BloggingApp.Web.Models.Domain.BlogPost", b =>
                {
                    b.Navigation("BlogPostComments");

                    b.Navigation("BlogPostLikes");
                });
#pragma warning restore 612, 618
        }
    }
}

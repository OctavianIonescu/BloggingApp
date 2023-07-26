using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BloggingApp.Web.Migrations.BloggingAppDb
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BlogPosts",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Author = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    heading = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    pageTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    shortDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    featuredURL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    URLHandle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    publishedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    visible = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogPosts", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    displayName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "BlogPostComment",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BlogPostID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateAdded = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogPostComment", x => x.ID);
                    table.ForeignKey(
                        name: "FK_BlogPostComment_BlogPosts_BlogPostID",
                        column: x => x.BlogPostID,
                        principalTable: "BlogPosts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BlogPostLike",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BlogPostID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogPostLike", x => x.ID);
                    table.ForeignKey(
                        name: "FK_BlogPostLike_BlogPosts_BlogPostID",
                        column: x => x.BlogPostID,
                        principalTable: "BlogPosts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BlogPostTag",
                columns: table => new
                {
                    blogPostsID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    tagsID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogPostTag", x => new { x.blogPostsID, x.tagsID });
                    table.ForeignKey(
                        name: "FK_BlogPostTag_BlogPosts_blogPostsID",
                        column: x => x.blogPostsID,
                        principalTable: "BlogPosts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BlogPostTag_Tags_tagsID",
                        column: x => x.tagsID,
                        principalTable: "Tags",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BlogPostComment_BlogPostID",
                table: "BlogPostComment",
                column: "BlogPostID");

            migrationBuilder.CreateIndex(
                name: "IX_BlogPostLike_BlogPostID",
                table: "BlogPostLike",
                column: "BlogPostID");

            migrationBuilder.CreateIndex(
                name: "IX_BlogPostTag_tagsID",
                table: "BlogPostTag",
                column: "tagsID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BlogPostComment");

            migrationBuilder.DropTable(
                name: "BlogPostLike");

            migrationBuilder.DropTable(
                name: "BlogPostTag");

            migrationBuilder.DropTable(
                name: "BlogPosts");

            migrationBuilder.DropTable(
                name: "Tags");
        }
    }
}

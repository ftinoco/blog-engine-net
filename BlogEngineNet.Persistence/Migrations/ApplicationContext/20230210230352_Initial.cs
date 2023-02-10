using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BlogEngineNet.Persistence.Migrations.ApplicationContext
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Blog");

            migrationBuilder.CreateTable(
                name: "User",
                schema: "Blog",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    Username = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Post",
                schema: "Blog",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    Title = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Content = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    AuthorId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Post", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Post_Author",
                        column: x => x.AuthorId,
                        principalSchema: "Blog",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comment",
                schema: "Blog",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    Content = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                    PostId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Post_Comment",
                        column: x => x.PostId,
                        principalSchema: "Blog",
                        principalTable: "Post",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PostTracking",
                schema: "Blog",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    PostId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReviewerId = table.Column<int>(type: "int", nullable: false),
                    PostStatus = table.Column<int>(type: "int", nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                    LastStatus = table.Column<bool>(type: "bit", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostTracking", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Post_Tracking",
                        column: x => x.PostId,
                        principalSchema: "Blog",
                        principalTable: "Post",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "Blog",
                table: "User",
                columns: new[] { "Id", "CreatedBy", "FirstName", "LastModifiedBy", "LastModifiedDate", "LastName", "Password", "Role", "Username" },
                values: new object[,]
                {
                    { 1, "Test", "John", "Test", null, "Doe", "SafePassword", 0, "JDoe" },
                    { 2, "Test", "Freddy", "Test", null, "Jackson", "SafePassword", 1, "FJackson" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comment_Id",
                schema: "Blog",
                table: "Comment",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Comment_PostId",
                schema: "Blog",
                table: "Comment",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_Post_AuthorId",
                schema: "Blog",
                table: "Post",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Post_Id",
                schema: "Blog",
                table: "Post",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PostTracking_Id",
                schema: "Blog",
                table: "PostTracking",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PostTracking_PostId",
                schema: "Blog",
                table: "PostTracking",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_User_Id",
                schema: "Blog",
                table: "User",
                column: "Id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comment",
                schema: "Blog");

            migrationBuilder.DropTable(
                name: "PostTracking",
                schema: "Blog");

            migrationBuilder.DropTable(
                name: "Post",
                schema: "Blog");

            migrationBuilder.DropTable(
                name: "User",
                schema: "Blog");
        }
    }
}

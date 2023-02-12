using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlogEngineNet.Persistence.Migrations.ApplicationContext
{
    /// <inheritdoc />
    public partial class AddingPublicUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "Blog",
                table: "User",
                columns: new[] { "Id", "CreatedBy", "FirstName", "LastModifiedBy", "LastModifiedDate", "LastName", "Password", "Role", "Username" },
                values: new object[] { 3, "Test", "Richard", "Test", null, "Ashcroft", "SafePassword", 2, "Ashcroft" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "Blog",
                table: "User",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}

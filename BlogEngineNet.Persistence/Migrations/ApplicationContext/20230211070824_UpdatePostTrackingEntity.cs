using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlogEngineNet.Persistence.Migrations.ApplicationContext
{
    /// <inheritdoc />
    public partial class UpdatePostTrackingEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ReviewerId",
                schema: "Blog",
                table: "PostTracking",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ReviewerId",
                schema: "Blog",
                table: "PostTracking",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}

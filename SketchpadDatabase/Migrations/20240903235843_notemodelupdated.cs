using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SketchpadDatabase.Migrations
{
    /// <inheritdoc />
    public partial class notemodelupdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "chatId",
                table: "Notes",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "chatId",
                table: "Notes");
        }
    }
}

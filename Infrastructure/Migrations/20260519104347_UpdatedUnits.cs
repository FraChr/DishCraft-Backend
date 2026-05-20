using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DishCraft.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedUnits : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Abbreviation",
                table: "Units");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Abbreviation",
                table: "Units",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}

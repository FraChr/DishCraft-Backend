using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DishCraft.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class update_Recipe : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Instruction",
                table: "Recipes",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Instruction",
                table: "Recipes");
        }
    }
}

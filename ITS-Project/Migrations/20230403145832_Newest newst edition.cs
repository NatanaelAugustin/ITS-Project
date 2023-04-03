using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ITS_Project.Migrations
{
    /// <inheritdoc />
    public partial class Newestnewstedition : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Author",
                table: "Comments");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Author",
                table: "Comments",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}

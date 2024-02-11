using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskPro_back.Migrations
{
    /// <inheritdoc />
    public partial class delete_is_deleted : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "tasks");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "tasks",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}

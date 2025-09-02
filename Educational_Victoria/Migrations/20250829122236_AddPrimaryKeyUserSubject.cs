using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Educational_Victoria.Migrations
{
    /// <inheritdoc />
    public partial class AddPrimaryKeyUserSubject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AccessId",
                table: "UserSubjectsAccesses",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccessId",
                table: "UserSubjectsAccesses");
        }
    }
}

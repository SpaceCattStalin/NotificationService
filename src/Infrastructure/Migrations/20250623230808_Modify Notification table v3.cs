using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ModifyNotificationtablev3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Notifications",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "Unread",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldDefaultValue: "Sent");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Notifications",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "Sent",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldDefaultValue: "Unread");
        }
    }
}

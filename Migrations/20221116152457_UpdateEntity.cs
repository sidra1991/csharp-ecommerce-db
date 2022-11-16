using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace csharpecommercedb.Migrations
{
    /// <inheritdoc />
    public partial class UpdateEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Prince",
                table: "Products",
                type: "float",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "Prince",
                table: "Products",
                type: "bit",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");
        }
    }
}

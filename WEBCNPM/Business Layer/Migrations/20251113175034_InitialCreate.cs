using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Business_Layer.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "ShippedDate",
                table: "Order",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2025, 11, 19, 0, 50, 33, 856, DateTimeKind.Local).AddTicks(9542),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2024, 7, 20, 7, 41, 40, 851, DateTimeKind.Local).AddTicks(1370));

            migrationBuilder.AlterColumn<DateTime>(
                name: "RequiredDate",
                table: "Order",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2025, 11, 17, 0, 50, 33, 856, DateTimeKind.Local).AddTicks(9309),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2024, 7, 18, 7, 41, 40, 851, DateTimeKind.Local).AddTicks(1169));

            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderDate",
                table: "Order",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 11, 14, 0, 50, 33, 856, DateTimeKind.Local).AddTicks(8932),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 15, 7, 41, 40, 851, DateTimeKind.Local).AddTicks(905));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "ShippedDate",
                table: "Order",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2024, 7, 20, 7, 41, 40, 851, DateTimeKind.Local).AddTicks(1370),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2025, 11, 19, 0, 50, 33, 856, DateTimeKind.Local).AddTicks(9542));

            migrationBuilder.AlterColumn<DateTime>(
                name: "RequiredDate",
                table: "Order",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2024, 7, 18, 7, 41, 40, 851, DateTimeKind.Local).AddTicks(1169),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2025, 11, 17, 0, 50, 33, 856, DateTimeKind.Local).AddTicks(9309));

            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderDate",
                table: "Order",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 15, 7, 41, 40, 851, DateTimeKind.Local).AddTicks(905),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 11, 14, 0, 50, 33, 856, DateTimeKind.Local).AddTicks(8932));
        }
    }
}

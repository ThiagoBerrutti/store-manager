using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StoreAPI.Migrations
{
    public partial class stockquantity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Count",
                table: "ProductStocks",
                newName: "Quantity");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "77edfe02-acf0-45f5-a904-5d8c4d756711");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "c6133367-ffbd-4a7c-9806-55920fda2181");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "390d0f13-7207-4efc-984f-e45c6e7b383e");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 4,
                column: "ConcurrencyStamp",
                value: "17a18e4c-cd4f-442a-accb-39478beb6a30");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a1c14f1f-2b78-444d-af30-2a58ad849b7f", "AQAAAAEAACcQAAAAECj24mk4633h33WnIHpRDzBwAvl3XVAzJYRtA25uNk8yVoyreLGy1FUZqAzJ4Ws5gQ==", "8d042e78-3d0a-4413-901d-efacc897caea" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0eb7ec2c-9823-4b0d-8e3b-beb0476b8e17", "AQAAAAEAACcQAAAAEG2pKP9FIJN0525YCa6cD9A/PL6+lu8l15i7obq4j4ecuDGNvVu0xTGn2RpU2bPamQ==", "c7b29375-2568-407c-b0c8-cb02a080c50b" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0409da33-360b-4c4b-99db-94a1fe4829ae", "AQAAAAEAACcQAAAAEDi6h7GDeTUPYTqAHARqRC/1Th1CyMnpciWo4/VNGFGemrqgM3k/SghzUMMTTWWlJg==", "11efa921-6c79-4811-85fc-5c4df83335a7" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "015f8936-7e5e-47a1-ab56-a2c975446241", "AQAAAAEAACcQAAAAEEzXWbmAloWBepqbB1keXbir82+MZgl4Rfxmq3nv0s9G/A0oesrCcCyJwg3cYYy+Mw==", "aeefa2a4-1e15-4228-bf94-51a8d206a430" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "ConcurrencyStamp", "DateOfbirth", "PasswordHash", "SecurityStamp" },
                values: new object[] { "00ab7071-3f5c-43cd-9100-3cf308a8ccb4", new DateTime(2002, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "AQAAAAEAACcQAAAAED539VxlsUuVlfXn2omj7usojA8TMnhyJYyRuL+UEY3uaCgQB80fd+KaJJvsBm0TsA==", "9c48e42c-afc9-467d-9184-56f4711e384a" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "ProductStocks",
                newName: "Count");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "0213ff49-b092-4d3a-b5b8-3dc5c0620bd3");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "837a7088-9c19-4f31-9f1c-c5d928817a6d");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "415e89e1-7636-49e2-bab9-1228b77107d0");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 4,
                column: "ConcurrencyStamp",
                value: "a3e5b554-9ecf-4895-96fe-be0beb6aaa63");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "51ab7670-84ea-4b96-bb4e-59dda974bfc4", "AQAAAAEAACcQAAAAEHRMcvXEKA3HpdmIfo+W+81pquJjBvyEnWBCUalgioLohZXfaN/2AyLLBg46a1Kh0g==", "46fc98c9-9d1b-41ab-aa23-0de854030537" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "568381c5-9ae5-4011-baa2-4ac99da413cc", "AQAAAAEAACcQAAAAEGEyobFB0KROhTM2MOG9f+pq7DaGH1ztiUaaqaXEBDPkLq/J+ofgee20l6jTJmncEA==", "e9610fd2-f8c9-4b92-bb88-9dc67fc8fad7" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1c263605-d634-4228-afae-2987b2ebc21a", "AQAAAAEAACcQAAAAEP3c802mXm8tQA+o7dtBjZCnU/ia/Qd8I4Q9YCpCcZF0PcTlexqGFpv7iBDf7oo9ZA==", "6e16f2e0-e6fb-4626-b2da-17fd5e7a2a7c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "da54594f-69ae-4abb-9773-b08cad1a686e", "AQAAAAEAACcQAAAAEBGHoiZB3KnPslz8Pp12Z6IudDGvyyOjwOXfZD+j/riFS5ck4R+5PB5nEGJB7xY4AQ==", "99ffaf32-a06a-44e5-8d6b-4c6823214165" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "ConcurrencyStamp", "DateOfbirth", "PasswordHash", "SecurityStamp" },
                values: new object[] { "bd6bcecf-008d-4dd3-b6dc-26ce81929b90", new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "AQAAAAEAACcQAAAAEK8Z1djRlhQutDTgsmtOiW9qhtSG7vDXXewuJxyFNur/HoEgY+u4KKSziYdY15F7zw==", "7d4be3f4-afe8-4834-a495-50e180ef00d1" });
        }
    }
}

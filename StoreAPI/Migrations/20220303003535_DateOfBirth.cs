using Microsoft.EntityFrameworkCore.Migrations;

namespace StoreAPI.Migrations
{
    public partial class DateOfBirth : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateOfbirth",
                table: "AspNetUsers",
                newName: "DateOfBirth");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "08c22229-d5db-41ee-b1ee-fbd355ee887a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "dd3ccd0c-a060-4ea8-8b1e-9f5715c220ff");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "29d544c6-5227-4dff-8e95-47203343b441");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 4,
                column: "ConcurrencyStamp",
                value: "d206fff3-559d-4cd6-b0ca-1c75c953e307");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3f452be5-4a25-438b-8ca0-227d413acb5e", "AQAAAAEAACcQAAAAEGSz+lXxfv1bBBp3iHVnI1lAAdvwTOAsvHuQxewmOE7c91ecDrNafwMK1+PFWQG9sw==", "e8c74dd2-15b7-4341-81fc-10547d9be605" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c4a8e1ab-aed2-4809-abb1-c83b52b9ae7a", "AQAAAAEAACcQAAAAEMlvFryFk7TcTIatsQI0BeyYl/+dZBfXE5LU6rXzjfc7Bho1PVYC+DkACte3E+ye0w==", "3fcf75b9-f74e-41af-aa38-a83286d77110" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "89745077-89f8-4a33-8d04-60bbae83028a", "AQAAAAEAACcQAAAAEHjcaMKvr5Qgs/2UnO8LH5g3d50qTbG0TpoZTgFmtiZTk15lsTcvnh6a/PyLKGpaJg==", "0f144ae4-8466-4c05-9eae-c8241cf91c4b" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e4a8cdc4-7cc6-4f0a-be9b-ad8ce8ffdf55", "AQAAAAEAACcQAAAAEGNU1ihW8ikd+Sochk9nAofCmakMFuvHypzqq3JfZYjNypcCBFV+uj8JpFnaT0wHmw==", "e95fdd94-87c0-4263-9caf-16f5be626f6d" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0efe9cce-a591-4675-a7fc-00fec31b230a", "AQAAAAEAACcQAAAAEHukzt3XBk2PGx6kIzINmvgjEl8TzcrmI6E/gYBWTCknk0L+p+ohaujv6EZ55VNGmQ==", "54c283d2-e7a2-4033-9e05-d57a44cb99c0" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateOfBirth",
                table: "AspNetUsers",
                newName: "DateOfbirth");

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
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "00ab7071-3f5c-43cd-9100-3cf308a8ccb4", "AQAAAAEAACcQAAAAED539VxlsUuVlfXn2omj7usojA8TMnhyJYyRuL+UEY3uaCgQB80fd+KaJJvsBm0TsA==", "9c48e42c-afc9-467d-9184-56f4711e384a" });
        }
    }
}

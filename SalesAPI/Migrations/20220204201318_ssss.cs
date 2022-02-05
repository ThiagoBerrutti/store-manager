using Microsoft.EntityFrameworkCore.Migrations;

namespace SalesAPI.Migrations
{
    public partial class ssss : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "a0bebba1-8659-49ee-9694-4854f2af5d19");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "da769446-77ee-4fb7-886d-d7faf628aae7");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "67a209c0-09bc-43b2-b89e-a2e2bfb7917f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 4,
                column: "ConcurrencyStamp",
                value: "303f645a-6aaa-4168-a37b-29f659eafaf1");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "270b4c20-4573-4ca9-800a-2e27360087da", "AQAAAAEAACcQAAAAEJ86z5UC9vyrmwgO54xz+ohpxEUR85oRmXsioLbxVpDlfyZdQQXoxMgcMp1dtVG7Lg==", "b6263d56-178a-4b09-9fb7-5c81694d7386" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4eda3e1f-8b1d-441d-a7e2-7d374458bcc2", "AQAAAAEAACcQAAAAEGYBSNaN+4i1vPPbL4VxM3SRRfAxUTrTXbBYYkPoR8GWoQu7nvEX3DNE5za/MeYfKA==", "f93f5b9c-b48d-4e01-9802-9e84f0e411da" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d16b0075-74ca-42a9-aead-fcd99dce147f", "AQAAAAEAACcQAAAAEOjfw364r6LQSH+GosNTMLYBMq2FqvyTXDBBWITdweba7V2b9dpzhqW2s9WW69dYKQ==", "1ccdc42a-5940-4f4f-8402-f6b12f910600" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3928be8f-26c8-4221-9532-c5213bacd98c", "AQAAAAEAACcQAAAAEFqSmcYSF6kIq05i/st06E9njzGjfmU/gdDbyO24VLHQ+cRR/FtnzQo/duPpXO7cUA==", "0e363175-3529-436c-8eb2-04b7ff219f30" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BaseSalary = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "c60f4ba0-cd92-4749-ad79-ffe6be967cb9");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "d9c14641-8792-46b3-a17e-17ab5ace6110");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "73f84461-3c53-4433-bbb0-3c3a08021314");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 4,
                column: "ConcurrencyStamp",
                value: "d7ec5d8b-207d-4dcb-9b1e-8b1fd4956432");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "58febdd3-9a1d-4510-a13b-f314646085ed", "AQAAAAEAACcQAAAAEMIJTgPSEVv/wIDQwrd0N3UgHsp0bomLru8ZmIqayQENqp4VEk91ockcylce5sIgpw==", "abc7e1e6-d3d4-45d5-9068-dae5b84c4538" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b89ec279-e8b1-4e2e-a9ca-e33bfe7bd8f6", "AQAAAAEAACcQAAAAEOhQk+4xn0iF0a5KSMX2ePa7DwXb1rYFN95u8TyEqrRX8YMnMzoVJnjGkz0zxKdPpA==", "fc323f65-36c0-453c-80e4-39c6fc0ad22a" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "18d8dabf-2620-4789-8a66-a4a2ec34bf47", "AQAAAAEAACcQAAAAEAV01UQm3+97szZvCtOO/o5Q1il3DSPCK9mDP8nvIJfzZMvfu/F79A6EuJE5xbPjOA==", "29ff645e-11f7-43a2-9098-902aad8ced5b" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "706ec659-b005-417e-9178-d2214b20edd5", "AQAAAAEAACcQAAAAEEY8kLEbiKjsXaKXRsFS/RSV/m8O8rXu4kmS17rK9PL+PTvEyXoYfeFkFENOTes/zg==", "d17b3b63-b182-4821-b96c-c9ba6cbc9ace" });
        }
    }
}

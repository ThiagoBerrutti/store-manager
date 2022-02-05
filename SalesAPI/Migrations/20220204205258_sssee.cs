using Microsoft.EntityFrameworkCore.Migrations;

namespace SalesAPI.Migrations
{
    public partial class sssee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "d59281db-6223-4f3c-8e60-6380ea24add7");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "b997c4f6-b471-409d-a7dc-dcb20c0c1da1");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "5d2aca51-dcf5-423f-8cf0-40164e187580");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 4,
                column: "ConcurrencyStamp",
                value: "a1131f3a-4bc5-4667-b09b-72f7cd173146");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "82d51933-2050-44a4-875e-17a7bbc598a4", "AQAAAAEAACcQAAAAECJPEgWMmEZ7mY4PwdPSs+3dxMoWAFWIvsgdu6IZKiHJE5iAC3tLAF6EOFl1qS1LRw==", "16a8a805-295e-44df-9a1f-e272ee00b3e5" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4fbaa2fa-2348-49ac-b3ba-0eb58d63d2be", "AQAAAAEAACcQAAAAEGWB8P3cHtL82eFIFB1YjfwyVym/BPV5SRkH6T9hnjWiMugdXsBQDOMqZ7bmdbG6wA==", "d69de589-7828-41c8-9646-c945c572da17" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5a36ad24-49a9-4c52-8299-e03af3edd3a8", "AQAAAAEAACcQAAAAEDRMeQ7Eca+j2NUxr7Ogc5lSlP6tuz5OUnar8N+xw1r7bUeAF6EaQFYDNYsx6v1frw==", "30b2dc93-8d5e-4fb8-bd45-014308057ab9" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2fc1ffac-142e-47f6-b358-a55c71fd5869", "AQAAAAEAACcQAAAAEK0z5pKVG5sPtlM8FKLgkqwGT5YD5b7PGbBECcVl4iY4Dv3mENn527jy7xxVNoqUXA==", "774b115b-0029-405f-a562-e67d6760f20e" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
    }
}

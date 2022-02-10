using Microsoft.EntityFrameworkCore.Migrations;

namespace SalesAPI.Migrations
{
    public partial class sss : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "Count",
                table: "ProductStocks",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "9012779f-28cd-44e2-87e0-180caddef8e6");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "6cabb007-28e1-425b-9840-3a0f08a3663a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "e15b8847-719a-4f16-8c50-5afce6d564a0");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 4,
                column: "ConcurrencyStamp",
                value: "dbb38a83-6806-4203-8109-6b03be6e54fb");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c016e000-c5f1-4bd5-bc2f-4039635aecd0", "AQAAAAEAACcQAAAAEBYOtfejynu2ywvd9IDfGoO/b5mIhSVTGzx5kQYowGgRHAp2X/qZBkdcxCG7VGnORw==", "daa01763-f3ab-4dd4-9691-003571b4b5fa" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "FirstName", "LastName", "PasswordHash", "SecurityStamp" },
                values: new object[] { "394afc43-9e34-4ea4-82f4-1fb9d2d510ec", "Manager", "Test Acc", "AQAAAAEAACcQAAAAEJtJEEzEUBdeFSXOPBiDMMs9S1EH7cIcw5oflhdEXSpBqfQVAuqWCBwb0NcjCHUB/A==", "c4cbcd58-02e1-4fa7-aee2-b14095bc3701" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "FirstName", "LastName", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0a66e70f-5a3a-4402-80c8-d96c8f78628c", "Stock", "Test Acc", "AQAAAAEAACcQAAAAEPDbymmfnbmN6l0qkVdva8Llr2oC2U9Wl7nAS0scxbxzmdhFzPw708DLq5tnI6c9FQ==", "e2da26dd-8bd8-4b1c-9827-d90249b26ecc" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "ConcurrencyStamp", "FirstName", "LastName", "PasswordHash", "SecurityStamp" },
                values: new object[] { "aa548cf3-e37b-46c9-b086-c26fd035d9f7", "Seller", "Test Acc", "AQAAAAEAACcQAAAAEJBIAZyhSZN/Q8gbaGGHvCXWoW+ZnmuVzSfEWIQuGgEkXQsoiePLPDlKnvPd6e4Pzg==", "1009dd48-02a2-4b8e-9ffe-0f0af9199b8d" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Count",
                table: "ProductStocks",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "9ae0aaa3-ca8e-436e-9224-1640c9e66de7");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "8bdbb723-724b-42be-85b1-35a6c6639b8d");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "36c7455a-253e-4ef5-9eea-5678a145cb85");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 4,
                column: "ConcurrencyStamp",
                value: "15a5359f-9dca-4421-949a-b0f0cc66bfca");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4efd3b8d-1c1f-4303-99a4-865b3e2228dc", "AQAAAAEAACcQAAAAEEqACSKQng61h2/EmH2p8TfdnojLEmTYXBZo/t3uhG6xclM02o9oNzQUjNILHgU6PA==", "30d7fbc8-28ca-4203-838d-aa00724140fe" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "FirstName", "LastName", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1fca9c45-4673-4943-bc7b-d3fa08ba0230", "Bruno", "Bento", "AQAAAAEAACcQAAAAEIrLGvY1FiXZ6H9liM/XDiwqH4cgGypKs6WoAwxnaA4JPX67nVsRiJmtxGS/x+ZJLw==", "a9d35060-fb4b-4884-9a96-81d4df254de8" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "FirstName", "LastName", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a9a548a7-f0d0-4790-9f02-c9b9d56d3f22", "Carlos", "Costa", "AQAAAAEAACcQAAAAEDeqXm7PoKc9dcuju8Tasj90CjCXe5RVldnjvqH/mn6j8bXmA0jlgEkDepeCsHQxQQ==", "e85beb18-361f-49e7-a854-0b2f68b6ef71" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "ConcurrencyStamp", "FirstName", "LastName", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2223364d-d60a-4364-b10d-b7d7b948f042", "Diego", "Dourado", "AQAAAAEAACcQAAAAEFSm8eNGC0FAGiWwNAgcyjtGORh7/oa9/PI/0p6hPK4yChEo932mEkh2ol/hiYJR/Q==", "f45381de-ef0c-4577-a502-d50bfd90feb3" });
        }
    }
}

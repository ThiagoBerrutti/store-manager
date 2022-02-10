using Microsoft.EntityFrameworkCore.Migrations;

namespace SalesAPI.Migrations
{
    public partial class sss1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                value: "b2f7ad43-4a23-480c-ba5b-5ea82d9be98b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "cb096913-61f0-433b-9c5e-a4fcef049e97");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "7d9ee906-b727-4bb2-95dd-67913191f6ae");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 4,
                column: "ConcurrencyStamp",
                value: "599b1485-9984-453d-ab86-d7b8132355a6");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "37a6896b-77a0-44a2-b2b2-f1240a5cb16f", "AQAAAAEAACcQAAAAEHHSfeRsTym+HRfcwmIT+yb88MuxwbfkVuM2Fsr7MD3p9CacRe2MTGeEDIl9yIhEFg==", "8a29c460-83ec-4ee7-ad73-04ef66870714" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a755b669-d985-4046-9440-da4e15b54cac", "AQAAAAEAACcQAAAAEIlXoGH7Xa01jtwAZyloRaDzHfFMBaoGQMLIpUxn1tIE4YrkCtQ2uL2RXbVmxLQQSA==", "5dee8ab1-f650-43b9-aad8-d9a8de6a3af0" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "cbba7273-bcdb-4ac9-b838-8b7eff1ed083", "AQAAAAEAACcQAAAAEGVPh8J7UOWFm/R2+IJ576loPk9aUpIwtHwMmQ3ld0F+GICSBPBO59w99Eo1TN28qQ==", "3cb11a78-da05-4d22-9dca-0933ab39ed46" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "18bfb202-6ceb-4bcd-8ea8-645aef798001", "AQAAAAEAACcQAAAAEOkpXXeJyJ9UsTVleuSuWQa4II791Orj63rZdFw2knBxemfDi2kTjDnC3YVBDLCQEA==", "c6a2992f-d5fe-4a76-8271-c713619e21c9" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "394afc43-9e34-4ea4-82f4-1fb9d2d510ec", "AQAAAAEAACcQAAAAEJtJEEzEUBdeFSXOPBiDMMs9S1EH7cIcw5oflhdEXSpBqfQVAuqWCBwb0NcjCHUB/A==", "c4cbcd58-02e1-4fa7-aee2-b14095bc3701" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0a66e70f-5a3a-4402-80c8-d96c8f78628c", "AQAAAAEAACcQAAAAEPDbymmfnbmN6l0qkVdva8Llr2oC2U9Wl7nAS0scxbxzmdhFzPw708DLq5tnI6c9FQ==", "e2da26dd-8bd8-4b1c-9827-d90249b26ecc" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "aa548cf3-e37b-46c9-b086-c26fd035d9f7", "AQAAAAEAACcQAAAAEJBIAZyhSZN/Q8gbaGGHvCXWoW+ZnmuVzSfEWIQuGgEkXQsoiePLPDlKnvPd6e4Pzg==", "1009dd48-02a2-4b8e-9ffe-0f0af9199b8d" });
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace StoreAPI.Migrations
{
    public partial class changedAge : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "50de6b76-be91-4761-872d-adc07ca56dce");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "4c40d4f3-9a9d-46c2-a1db-4f4ef2cbd9c0");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "dc88c126-d009-4f7f-aa3e-350265367fe7");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 4,
                column: "ConcurrencyStamp",
                value: "9bcd9f64-fc4f-4a19-b390-ac44b1dc9d69");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Age", "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { 42, "a229eadf-a682-43d2-9ae1-8d6bd6fcb05a", "AQAAAAEAACcQAAAAEDanYP35432V9tiXygxwNxZ4z0oqcEB3Ub32oMXapbAFq/ltkTI4hBUWGu1JZMJGTQ==", "ff89542f-8ea8-4397-9922-4c95256c54b7" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Age", "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { 32, "a7ae8b5f-9401-43f6-8e27-08bf6dfb7eca", "AQAAAAEAACcQAAAAEJRoMYhsCbQyafWgODFXdqWzLQKXcu90ATWVd4ZDG/EUGGelyZ4J4McA3jn0eamwTw==", "ac5c8ee8-cb6a-4f24-9588-fb2612274171" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Age", "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { 27, "dc8f0419-9a0e-47a0-bbe1-06aa045a3b96", "AQAAAAEAACcQAAAAELMf0ZjHsqPT0awa2aIrmBJpNDTkKvcmhyyPItW+KVr+zBnBFtq4namlWNTVahkAxw==", "964c25a6-52cf-40fd-9b22-95401a9a7fed" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Age", "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { 22, "99662089-a8ec-4e0b-bf58-8c411ca3ba76", "AQAAAAEAACcQAAAAEC4GJawPeH9MSev2Vb/m2PWgxyD7Y+wzu1eORtyMPE5KBS3xf4mChqrGjoV7pUaI4Q==", "6159f18f-3fc7-49cc-9a78-4d1e6fae3c2d" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Age", "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { 20, "b871f56f-0871-4c7b-b3cc-d604816c3c66", "AQAAAAEAACcQAAAAEHb5eqGLDxb8NiObLDpk7X5M39jbykfxbrE/E10nO6MtWmGR+xX0U7qt/8llfJ8Nqg==", "56987fbe-b214-45d6-856a-8911f404e124" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "7277e3de-a1c0-46c4-b91c-f523f1d33373");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "406d43b7-3110-4648-b75e-646d4227d0cd");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "cc443e1c-44e9-4712-a464-65b71be2a8a9");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 4,
                column: "ConcurrencyStamp",
                value: "fb0afe90-c12c-427c-8e0d-9a59047b86cf");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Age", "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { 2021, "2c5d047f-22f0-418a-b466-29d6aba22f84", "AQAAAAEAACcQAAAAEHC80ozFBebkCtnLZgVFQ96ngM+uUfk/thLVreJ+f5vjd68Eia453HrYi5UPAeLXNA==", "964942a9-c1f8-4784-bd7b-70522dcf5394" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Age", "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { 2021, "50e7e2c4-2971-4e89-a07f-4f1a34252cf3", "AQAAAAEAACcQAAAAEJrQMkq72Zq2yuo51iSO3qBwHxUB0Xjixj8hpA+SQ2o4DSPruFjqQl+oBn/WDYxrFg==", "c78456e9-24d1-4637-abdd-6a9b150df395" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Age", "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { 2021, "91a8f219-a007-4311-baca-cbd63dedc098", "AQAAAAEAACcQAAAAEJS0AZfDHyzKUdQwQ/XxnQfOjb5DD2VIY7PatjW16OkezOtgrDLJ+0mfXf6ukRvepA==", "cb938dce-18e4-4307-bcd2-808607c06fa4" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Age", "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { 2021, "2df9a82f-912c-4264-9334-32a44edf2d54", "AQAAAAEAACcQAAAAEL6xbGjeBRE+6ud/sF7p163VGG2wo/jkoWKEz/lEYsm/cF690A/maSv20W1kaj5DFQ==", "f0d11e34-e3c6-4669-b666-e1761f70438f" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Age", "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { 2021, "9fe30e79-bf06-4978-8169-05869f299661", "AQAAAAEAACcQAAAAEG4VGhgvaPx0IRkk8LKHVdpGu3oubcyEw0Ja4sMVbR9bU3c7sqzWaJErlYZ3LlufiQ==", "acb32422-1ed6-4b99-a285-92a9f39737ff" });
        }
    }
}

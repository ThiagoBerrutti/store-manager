using Microsoft.EntityFrameworkCore.Migrations;

namespace StoreAPI.Migrations
{
    public partial class removedAge : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Age",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "daab5580-0ae1-42c5-84f3-6529770fa58e");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "fb96849a-113d-42f6-aef9-b612d77aed2f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "13595dc3-680f-49cc-9aed-5e0ef5b30b3a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 4,
                column: "ConcurrencyStamp",
                value: "bd7e54cb-b22c-4a71-b0de-7b9bcfaeab01");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0696d183-bad4-46d1-bba1-e02ba6a30fe3", "AQAAAAEAACcQAAAAEKM0jzhYAON3uy+oiQf3QN6yug56OFwnN7bYLZBwMLvFeDkKBaen/MmN1K67V97cxA==", "0b1fdf65-dacc-4cf4-a94f-0c22364370ce" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "55b016cc-6197-4acc-be70-72cfc5dcdbcf", "AQAAAAEAACcQAAAAEBFB9YY0hsvATAFRKlEbBhKDZI7Np1pb+5EolPatE2xbhrcicM4hyYTu5ob1LbHizg==", "20ca853f-62b9-414d-a24c-8e307ace3b24" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a73ad0f7-baa1-417c-8e3a-c9fe06113973", "AQAAAAEAACcQAAAAENtGhJ9m3udp+gGbgRk/7fbwUYlTDEhhpZkmZ0lVNTQ6waku3L+4YmLdwbIH5UulrQ==", "feee0aa7-0b79-4f4d-9038-511a0c653ec8" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1da1fbcd-5305-48c2-85a1-c2fdd79d9375", "AQAAAAEAACcQAAAAEC+AoRezR7cjc5P323lmabzCBCzYOsXiG2dlLTiE+jaQAnEYA4IhPfu7X+dM8wiGkQ==", "9cbb6011-c4dd-44c0-a40e-2644dcf0c122" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3ed1b2eb-9690-45a2-a76e-45b861f8242c", "AQAAAAEAACcQAAAAECuVpu4GhtlLkuAuOihZIS1GJDw4g9wcsqB/kSxQOhg31tFQxAn3HZ7UpCR/Yeiw7g==", "d4890dcc-4c6c-48f6-a3e0-8dd4829b4c75" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Age",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

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
    }
}

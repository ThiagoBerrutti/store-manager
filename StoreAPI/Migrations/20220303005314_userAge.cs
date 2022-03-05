using Microsoft.EntityFrameworkCore.Migrations;

namespace StoreAPI.Migrations
{
    public partial class userAge : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Age",
                table: "AspNetUsers");

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
    }
}

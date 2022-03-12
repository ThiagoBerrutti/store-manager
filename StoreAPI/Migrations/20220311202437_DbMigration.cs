using Microsoft.EntityFrameworkCore.Migrations;

namespace StoreAPI.Migrations
{
    public partial class DbMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "38e12d0f-227d-47c6-8ce6-e25354920f5d");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "e7fa4211-fb54-4256-a137-82daf1f53169");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "dd3cf533-e9c1-4a9e-97c8-584a87b305a3");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 4,
                column: "ConcurrencyStamp",
                value: "1e932ed6-6921-4ee1-857e-cf793e269dce");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c3f0ef67-721d-412f-abd0-c4ed2bbb65b2", "AQAAAAEAACcQAAAAEBT4fdxp5/xfJ4X4raZqaTFphx5LtezaWuc25coE6yeuDyd9OgrHZFYko+hvuY+Bag==", "2319e2d8-8c37-4157-91b2-d53f598dfb91" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "82a6a5c4-795a-4025-8832-8c36665e54bd", "AQAAAAEAACcQAAAAEDHDLTP1M7BHpThSQYCelR3KxmPK+qDuQoSIqV4rj2YVs42VpfFEPUb+N/dWci/oMA==", "9299021c-d3f3-4189-bd68-8268df4dc0cf" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "bac48320-5bb6-4b9e-8d0b-5b133ca6ada8", "AQAAAAEAACcQAAAAEFmy698chQ2E4E7dIygdZ5mz+xkHIk0GQcBnSP25gjgyoG2hn0InaDV+hWIAWzS9MA==", "ee61aa36-8f02-4395-9a60-d6b01f58fb36" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "40836bd8-ad96-4f5b-8362-6fb59297de37", "AQAAAAEAACcQAAAAEKTGncEPc28K/F20NIZvHo9gdp5CpV52OB4xBaUt0lv0d/+NcXY3zaFa2ybAmrMbmQ==", "1cc2ce74-88fe-49ba-8d69-81fb168250be" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "77436546-a249-40fc-8ba8-ba79f14d3386", "AQAAAAEAACcQAAAAEKh0lrjZ5apxtkiDrKVWb2kS0VhoW4++id79cQPaSgtq+eg8NY+VzXeh+K3Gd4S6hg==", "0a9aa26f-cf35-4d3f-a942-8e7f6865e56b" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "db2c551d-0fee-44b4-863a-6be198f666fb");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "00656978-2d68-4079-b9a7-1281d097f3d9");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "9008b5ca-607d-49cd-8042-bf0bb7226a01");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 4,
                column: "ConcurrencyStamp",
                value: "7bc5b401-1178-415b-b246-405586177ad8");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a49352cb-77fc-44eb-a9c7-f4587f04f4c7", "AQAAAAEAACcQAAAAEDEVNPPPRd4sfUzyjG96TTKlNGfLRkYzl+3VNaQwUF1ETkFH2OAk+XhwnUqLOIgm3g==", "4fe77584-2fcb-403b-9bfb-d69c87609068" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5c86a511-eed0-4af7-8338-d9dc87c15013", "AQAAAAEAACcQAAAAEPRn85VnpYrjy8nXQRAqoGX1FJy7OYFLxgcY5nTRqDBOBr6uU2NsQwqnmUaRXBPhkA==", "919cb45c-a52c-4842-bc46-ddd8af7ff8a3" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d7b50af3-75d3-4201-9420-79b8d68f6d13", "AQAAAAEAACcQAAAAEJtvbsxpgeOqLXjIk8zyg9lYVWmnrg0xBY5dmS6a8UhVBXWYiA5vF/oDQZOPe6WIfg==", "e16c8c49-3630-4b82-b664-3345469fdfa2" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c472e178-b64e-4e57-b989-e43ebd600ff2", "AQAAAAEAACcQAAAAELy4ktP/hTNQLq48eMS49Yoe6LfXGyNMGIsoHhFanDfRcKfbpKw/2oHi81Hmkgg5CQ==", "d055594a-4c8b-4bc0-8a46-2203e0d24abb" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "567df567-fe9b-4d7c-b281-23696b75f479", "AQAAAAEAACcQAAAAEC+ztnvMM3cVNpWDYHvvH+Oj0G6sc6V7IVjEmEEy7la9kfd+bD05yVw9OYdL0euW1A==", "76be18a3-40ef-4754-a287-4c7a05dec6a9" });
        }
    }
}

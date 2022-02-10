using Microsoft.EntityFrameworkCore.Migrations;

namespace SalesAPI.Migrations
{
    public partial class productdesc2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                columns: new[] { "ConcurrencyStamp", "LastName", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4efd3b8d-1c1f-4303-99a4-865b3e2228dc", "Istrator", "AQAAAAEAACcQAAAAEEqACSKQng61h2/EmH2p8TfdnojLEmTYXBZo/t3uhG6xclM02o9oNzQUjNILHgU6PA==", "30d7fbc8-28ca-4203-838d-aa00724140fe" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1fca9c45-4673-4943-bc7b-d3fa08ba0230", "AQAAAAEAACcQAAAAEIrLGvY1FiXZ6H9liM/XDiwqH4cgGypKs6WoAwxnaA4JPX67nVsRiJmtxGS/x+ZJLw==", "a9d35060-fb4b-4884-9a96-81d4df254de8" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a9a548a7-f0d0-4790-9f02-c9b9d56d3f22", "AQAAAAEAACcQAAAAEDeqXm7PoKc9dcuju8Tasj90CjCXe5RVldnjvqH/mn6j8bXmA0jlgEkDepeCsHQxQQ==", "e85beb18-361f-49e7-a854-0b2f68b6ef71" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2223364d-d60a-4364-b10d-b7d7b948f042", "AQAAAAEAACcQAAAAEFSm8eNGC0FAGiWwNAgcyjtGORh7/oa9/PI/0p6hPK4yChEo932mEkh2ol/hiYJR/Q==", "f45381de-ef0c-4577-a502-d50bfd90feb3" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "bdac0b7c-0e16-473f-9c34-4e26a1cc6f19");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "78a5b155-6e65-473f-a593-37f48dc4a1f3");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "8e00a395-5f57-4be7-bb59-14771b39525f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 4,
                column: "ConcurrencyStamp",
                value: "15aafa8c-1d4e-497d-afc2-1e2d695838f3");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "LastName", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b776026b-4eb5-485d-a675-6f5fe5c9c159", "Almeida", "AQAAAAEAACcQAAAAEJ4r61tDKWlLUrgVTvHzUsXLFrTOXeN2/otSYX3iU4zm04UZUqLcajmoBrKRMesokw==", "266b23eb-2159-42f9-9ad6-2f5b46e81dc6" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a853eb8a-85cc-43f1-bf53-87d6094a8523", "AQAAAAEAACcQAAAAELN0pnC5L5PaP8jFb9rW6YBcywGf1GkgTKuCZbLAg1j18thuJCK6250izjJs5dBC9g==", "30dd3697-1da1-47e1-a136-e3f8488729ba" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6ffcb9aa-e847-489d-b5eb-f602a9588c61", "AQAAAAEAACcQAAAAEIQFfnlnK59KhZF6YC0gRO8CTWvQNi4SQwzefOcSkYVOIbPa+jonvSB7kDwxOlNNuw==", "4eaf6f70-b07b-47a9-90a6-d7b1266ffe87" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a785489b-3790-4239-912e-fb0fb5173a7a", "AQAAAAEAACcQAAAAEOhzVVLHGcjX4J6h65MQV7cYRYjfGls8G6kW/Duo+MHXwByQ7IqfF8fjaZSOwHZcjQ==", "d43e5c4c-95e6-4ac7-8d20-86f26f470b83" });
        }
    }
}

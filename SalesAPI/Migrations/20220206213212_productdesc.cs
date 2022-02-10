using Microsoft.EntityFrameworkCore.Migrations;

namespace SalesAPI.Migrations
{
    public partial class productdesc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

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
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b776026b-4eb5-485d-a675-6f5fe5c9c159", "AQAAAAEAACcQAAAAEJ4r61tDKWlLUrgVTvHzUsXLFrTOXeN2/otSYX3iU4zm04UZUqLcajmoBrKRMesokw==", "266b23eb-2159-42f9-9ad6-2f5b46e81dc6" });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Products");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "d436c23c-eeea-473a-8561-306f78d102cb");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "f80128b5-2ddf-4cac-a8f4-56792f3a3e53");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "78f454e6-273f-418a-91a8-a77d8295f3d1");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 4,
                column: "ConcurrencyStamp",
                value: "72087377-8454-47da-9bf1-ef8b3a17065b");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d33fe107-39e1-42f5-b758-ba4e514bd0fe", "AQAAAAEAACcQAAAAECclsCUNkmzJk7F5CfRkDqtcRL0BI1S3yM0a+BBLVVR4scMf0TPX79p3o6gUTcnerQ==", "b2af7c38-f3db-4c43-8aee-2c5163fe5e7a" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "099aeecb-793f-41be-8761-b02009baa8f4", "AQAAAAEAACcQAAAAEPnXljUt6glPtsNG6zKXMf+217/Jx46BkMuLOMaeJv8DV4MtUyPb/J2xOl2KqlRJtA==", "34049067-e9a1-44e2-897d-a88873428f58" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "cd5365b1-0ceb-42b4-9db5-c885e3fa486a", "AQAAAAEAACcQAAAAEKe7qXAGUFGVoQMzi8uV3ZYMMZnosd2gK/W2I1CFJGeaXCfNe52sL7o9VFlBvh039w==", "0a2cfa4f-c6c9-4c4f-ac5c-751a61068e24" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "89302b7a-5bbf-4ac5-b68c-ed0aa5f0b3e0", "AQAAAAEAACcQAAAAEBsTwQRb22Pj6mPHW50jDWtTbUV5ol9WsP6R1sMJ/5PPjJ2d/dWYSU1zYC1DDXxuzA==", "673c3bdf-48b7-4753-a282-50b07cab5d68" });
        }
    }
}

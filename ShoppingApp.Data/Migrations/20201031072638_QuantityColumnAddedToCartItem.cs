using Microsoft.EntityFrameworkCore.Migrations;

namespace ShoppingApp.Data.Migrations
{
    public partial class QuantityColumnAddedToCartItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "CartItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "09d115c2-ad8c-4a09-a86e-eb50369618df");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "5674ade4-85e2-4a9a-9590-1999d0ba167e");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f78f1139-a450-4571-b22a-64b99b8dca32", "AQAAAAEAACcQAAAAEHOO2Htrp3Lq0RjtWr9owPrNcCSWe8OukCmb1z6mGQHIhKNacH8l2WpdWhKSUWpjig==", "546f733b-1289-49b2-8b05-149178cf7f79" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "83817d79-c9df-4ffe-8c26-3703cc78c4dc", "AQAAAAEAACcQAAAAEF9qoR+LspyM2xuvUFX9U5jYRes5Wd3NfWOcgvYyoEzR/0fBb0pHgk74q0fZ/E7fVg==", "bb34e9aa-dd41-4363-8820-388a7f194053" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "CartItems");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "f0cbb1b0-78dc-4ac2-a43a-faac059cde96");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "f74e110d-68b4-439d-9f6c-804474244711");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c543dc47-3647-40e5-9af0-afc83823881f", "AQAAAAEAACcQAAAAEKjkaUmTA+Qv3zJ6focqnS8hpXJ1xjHDbtAcealtyAK0PPA3ABDRQCfLxxwYThTOug==", "44035bf7-4bb3-48dd-9a86-937fc7fbed91" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3145469c-9652-411d-b49e-dd12d89f4aa6", "AQAAAAEAACcQAAAAEDo8zQKHvI2EOo+r6bSmHbLkXwvf+8kgozg9stouJRges971iDzEcZd7D2b068vgdw==", "18a1d0bb-15ab-4ba8-9b3b-a7d964371bf7" });
        }
    }
}

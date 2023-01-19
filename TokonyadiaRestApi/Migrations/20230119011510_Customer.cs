using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TokonyadiaRestApi.Migrations
{
    /// <inheritdoc />
    public partial class Customer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "m_customer",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    customername = table.Column<string>(name: "customer_name", type: "NVarchar(48)", nullable: false),
                    phonenumber = table.Column<string>(name: "phone_number", type: "NVarchar(14)", nullable: false),
                    address = table.Column<string>(type: "NVarchar(100)", nullable: false),
                    email = table.Column<string>(type: "NVarchar(48)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_m_customer", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_m_customer_email",
                table: "m_customer",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_m_customer_phone_number",
                table: "m_customer",
                column: "phone_number",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "m_customer");
        }
    }
}

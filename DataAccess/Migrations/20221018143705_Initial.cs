using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(46)", maxLength: 46, nullable: false),
                    SecondName = table.Column<string>(type: "nvarchar(46)", maxLength: 46, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(62)", maxLength: 62, nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Admin" });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "Name" },
                values: new object[] { 2, "Manager" });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "Name" },
                values: new object[] { 3, "Client" });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "Email", "FirstName", "PasswordHash", "PasswordSalt", "RoleId", "SecondName" },
                values: new object[] { 1, "admin@gmail.com", "Adam", new byte[] { 134, 105, 144, 12, 249, 168, 255, 33, 212, 120, 3, 24, 113, 253, 168, 156, 236, 69, 52, 31, 50, 125, 203, 49, 139, 234, 181, 73, 252, 33, 114, 187, 68, 104, 220, 146, 127, 145, 187, 251, 206, 243, 168, 124, 190, 159, 249, 127, 12, 24, 94, 235, 65, 201, 163, 157, 87, 161, 157, 253, 46, 99, 70, 235 }, new byte[] { 2, 163, 147, 16, 170, 144, 93, 182, 73, 240, 224, 110, 219, 124, 226, 247, 32, 141, 150, 170, 136, 161, 83, 87, 153, 158, 142, 98, 66, 129, 210, 181, 220, 156, 159, 251, 213, 147, 100, 103, 219, 7, 188, 81, 86, 62, 33, 249, 225, 138, 185, 84, 152, 87, 155, 6, 4, 209, 242, 121, 44, 140, 196, 162, 253, 112, 230, 222, 16, 247, 160, 130, 124, 122, 248, 203, 252, 36, 104, 11, 241, 74, 108, 156, 167, 205, 76, 238, 73, 191, 160, 166, 234, 30, 175, 18, 127, 48, 126, 246, 142, 25, 205, 139, 94, 189, 176, 248, 12, 19, 203, 235, 73, 5, 115, 125, 24, 180, 91, 144, 48, 188, 22, 88, 29, 213, 183, 243 }, 1, "Adamson" });

            migrationBuilder.CreateIndex(
                name: "IX_Role_Id",
                table: "Role",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_User_Email",
                table: "User",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_Id",
                table: "User",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_User_RoleId",
                table: "User",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Role");
        }
    }
}

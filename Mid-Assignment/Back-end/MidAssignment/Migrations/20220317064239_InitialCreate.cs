using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MidAssignment.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Book",
                columns: table => new
                {
                    BookId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Author = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Book", x => x.BookId);
                    table.ForeignKey(
                        name: "FK_Book_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookRequest",
                columns: table => new
                {
                    RequestId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestByUserId = table.Column<int>(type: "int", nullable: false),
                    ProcessByUserId = table.Column<int>(type: "int", nullable: true),
                    DateOfRequest = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookRequest", x => x.RequestId);
                    table.ForeignKey(
                        name: "FK_BookRequest_User_ProcessByUserId",
                        column: x => x.ProcessByUserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_BookRequest_User_RequestByUserId",
                        column: x => x.RequestByUserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BookRequestDetail",
                columns: table => new
                {
                    RequestId = table.Column<int>(type: "int", nullable: false),
                    BookId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookRequestDetail", x => new { x.RequestId, x.BookId });
                    table.ForeignKey(
                        name: "FK_BookRequestDetail_Book_BookId",
                        column: x => x.BookId,
                        principalTable: "Book",
                        principalColumn: "BookId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookRequestDetail_BookRequest_RequestId",
                        column: x => x.RequestId,
                        principalTable: "BookRequest",
                        principalColumn: "RequestId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "CategoryId", "CategoryName" },
                values: new object[,]
                {
                    { 1, "Detective Book" },
                    { 2, "Comic" },
                    { 3, "Novel" }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "UserId", "Password", "RoleId", "UserName" },
                values: new object[,]
                {
                    { 1, "12345678", 1, "mrA" },
                    { 2, "12345678", 1, "mrB" },
                    { 3, "12345678", 2, "mrC" },
                    { 4, "12345678", 2, "mrD" },
                    { 5, "12345678", 2, "mrE" }
                });

            migrationBuilder.InsertData(
                table: "Book",
                columns: new[] { "BookId", "Author", "BookName", "CategoryId" },
                values: new object[,]
                {
                    { 1, "Conan Doyle", " Sherlock Holmes", 1 },
                    { 2, "Hailey", "Detective", 1 },
                    { 3, "Simon Spurrier", "CODA", 2 },
                    { 4, "Kurt Busiek", "MARVELS", 2 },
                    { 5, "Bram Stoker", "Dracula", 3 },
                    { 6, "J. R. R. Tolkien", "The Hobbit", 3 }
                });

            migrationBuilder.InsertData(
                table: "BookRequest",
                columns: new[] { "RequestId", "DateOfRequest", "ProcessByUserId", "RequestByUserId", "Status" },
                values: new object[,]
                {
                    { 1, new DateTime(2021, 8, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 3, 0 },
                    { 2, new DateTime(2022, 2, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 3, 2 },
                    { 3, new DateTime(2022, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 4, 1 }
                });

            migrationBuilder.InsertData(
                table: "BookRequestDetail",
                columns: new[] { "BookId", "RequestId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 1 },
                    { 3, 1 },
                    { 3, 2 },
                    { 4, 2 },
                    { 5, 3 },
                    { 6, 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Book_CategoryId",
                table: "Book",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_BookRequest_ProcessByUserId",
                table: "BookRequest",
                column: "ProcessByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BookRequest_RequestByUserId",
                table: "BookRequest",
                column: "RequestByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BookRequestDetail_BookId",
                table: "BookRequestDetail",
                column: "BookId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookRequestDetail");

            migrationBuilder.DropTable(
                name: "Book");

            migrationBuilder.DropTable(
                name: "BookRequest");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}

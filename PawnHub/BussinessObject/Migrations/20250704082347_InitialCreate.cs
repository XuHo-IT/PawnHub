using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BussinessObject.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CapitalInformation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TotalCapital = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalIncome = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalExpenditure = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalProfit = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CapitalInformation", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ShopInformation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShopName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ShopAddress = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Telephone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShopInformation", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ShopItem",
                columns: table => new
                {
                    ShopItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DateAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsExpired = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShopItem", x => x.ShopItemId);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UserRealName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Telephone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Dob = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    UserRole = table.Column<int>(type: "int", nullable: false),
                    CID = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserID);
                });

            migrationBuilder.CreateTable(
                name: "Bills",
                columns: table => new
                {
                    BillId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShopItemId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    DateBuy = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bills", x => x.BillId);
                    table.ForeignKey(
                        name: "FK_Bills_ShopItem_ShopItemId",
                        column: x => x.ShopItemId,
                        principalTable: "ShopItem",
                        principalColumn: "ShopItemId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Bills_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Item",
                columns: table => new
                {
                    ItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Value = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Interest = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsApproved = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Item", x => x.ItemId);
                    table.ForeignKey(
                        name: "FK_Item_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PawnContracts",
                columns: table => new
                {
                    ContractId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    LoanAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ContractDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PawnContracts", x => x.ContractId);
                    table.ForeignKey(
                        name: "FK_PawnContracts_Item_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Item",
                        principalColumn: "ItemId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PawnContracts_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "CapitalInformation",
                columns: new[] { "Id", "TotalCapital", "TotalExpenditure", "TotalIncome", "TotalProfit" },
                values: new object[] { 1, 10000.00m, 10000.00m, 0m, 0m });

            migrationBuilder.InsertData(
                table: "ShopInformation",
                columns: new[] { "Id", "EmailAddress", "ShopAddress", "ShopName", "Telephone" },
                values: new object[] { 1, "fpt@edu.vn", "FPT University", "FPT Pawn Shop", "1234-5555" });

            migrationBuilder.InsertData(
                table: "ShopItem",
                columns: new[] { "ShopItemId", "DateAdded", "Description", "IsExpired", "Name", "Price" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 6, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "High performance laptop for gaming.", true, "Gaming Laptop", 750.00m },
                    { 2, new DateTime(2025, 6, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Latest smartphone with advanced features.", true, "Latest Model Smartphone", 300.00m },
                    { 3, new DateTime(2025, 5, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Professional electric guitar.", true, "Electric Guitar", 450.00m },
                    { 4, new DateTime(2025, 6, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "High resolution digital camera.", true, "Digital Camera", 600.00m },
                    { 5, new DateTime(2025, 6, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "Luxury brand handbag.", true, "Designer Handbag", 850.00m }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "UserID", "Address", "CID", "Dob", "EmailAddress", "Gender", "Password", "Telephone", "UserName", "UserRealName", "UserRole" },
                values: new object[,]
                {
                    { 1, "123 Main St", "C123456789", new DateTime(1990, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "john.doe@example.com", "Male", "Password123", "123-456-7890", "john_doe", "John Doe", 1 },
                    { 2, "456 Oak St", "C987654321", new DateTime(1988, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "jane.smith@example.com", "Female", "Password456", "098-765-4321", "jane_smith", "Jane Smith", 2 },
                    { 3, "789 Pine St", "C555123456", new DateTime(1985, 10, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "michael.brown@example.com", "Male", "Password789", "555-123-4567", "michael_brown", "Michael Brown", 1 },
                    { 4, "101 Maple St", "C444987654", new DateTime(1992, 2, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "emily.jones@example.com", "Male", "Password101", "444-987-6543", "emily_jones", "Emily Jones", 2 },
                    { 5, "202 Oakwood Ave", "C333222111", new DateTime(1978, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "robert.smith@example.com", "Female", "Password202", "333-222-1111", "robert_smith", "Robert Smith", 1 }
                });

            migrationBuilder.InsertData(
                table: "Bills",
                columns: new[] { "BillId", "DateBuy", "ShopItemId", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 6, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1 },
                    { 2, new DateTime(2025, 6, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 2 },
                    { 3, new DateTime(2025, 6, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 3 },
                    { 4, new DateTime(2025, 6, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 4 },
                    { 5, new DateTime(2025, 6, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, 5 }
                });

            migrationBuilder.InsertData(
                table: "Item",
                columns: new[] { "ItemId", "Description", "ExpirationDate", "Interest", "IsApproved", "Name", "Status", "UserId", "Value" },
                values: new object[,]
                {
                    { 1, "14K Gold Ring", new DateTime(2025, 7, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), 0.05m, true, "Gold Ring", "Pending", 1, 250.00m },
                    { 2, "Luxury Watch", new DateTime(2025, 8, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), 0.10m, true, "Luxury Watch", "Active", 2, 500.00m },
                    { 3, "24K Diamond Necklace", new DateTime(2025, 9, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), 0.07m, true, "Diamond Necklace", "Pending", 3, 1200.00m },
                    { 4, "Sterling Silver Bracelet", new DateTime(2025, 7, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), 0.04m, false, "Silver Bracelet", "Active", 4, 150.00m },
                    { 5, "Porcelain Antique Vase", new DateTime(2025, 10, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), 0.08m, true, "Antique Vase", "Pending", 5, 750.00m }
                });

            migrationBuilder.InsertData(
                table: "PawnContracts",
                columns: new[] { "ContractId", "ContractDate", "ExpirationDate", "ItemId", "LoanAmount", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 5, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 7, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 200.00m, 1 },
                    { 2, new DateTime(2025, 4, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 8, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 400.00m, 2 },
                    { 3, new DateTime(2025, 3, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 7, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 900.00m, 3 },
                    { 4, new DateTime(2025, 5, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 7, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 120.00m, 4 },
                    { 5, new DateTime(2025, 2, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 7, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, 600.00m, 5 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bills_ShopItemId",
                table: "Bills",
                column: "ShopItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Bills_UserId",
                table: "Bills",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Item_UserId",
                table: "Item",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PawnContracts_ItemId",
                table: "PawnContracts",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_PawnContracts_UserId",
                table: "PawnContracts",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bills");

            migrationBuilder.DropTable(
                name: "CapitalInformation");

            migrationBuilder.DropTable(
                name: "PawnContracts");

            migrationBuilder.DropTable(
                name: "ShopInformation");

            migrationBuilder.DropTable(
                name: "ShopItem");

            migrationBuilder.DropTable(
                name: "Item");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}

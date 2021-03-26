using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RoBoToC.DataAccess.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "_Currencies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Currencies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "_OperationClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__OperationClaims", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "_Times",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Quantity = table.Column<short>(type: "smallint", nullable: false),
                    Duration = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Times", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "_Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: true),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Surname = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    RegistrationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "_Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ProfitRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BuyingHook = table.Column<bool>(type: "bit", nullable: false),
                    SellingHook = table.Column<bool>(type: "bit", nullable: false),
                    SellingHookRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    StopLoss = table.Column<bool>(type: "bit", nullable: false),
                    StopLossRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SpendRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    WalletAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK__Orders__Users_UserId",
                        column: x => x.UserId,
                        principalTable: "_Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "_UserApis",
                columns: table => new
                {
                    Market = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ApiKey = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApiSecret = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__UserApis", x => new { x.UserId, x.Market });
                    table.ForeignKey(
                        name: "FK__UserApis__Users_UserId",
                        column: x => x.UserId,
                        principalTable: "_Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "_UserOperationClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    OperationClaimId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__UserOperationClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK__UserOperationClaims__OperationClaims_OperationClaimId",
                        column: x => x.OperationClaimId,
                        principalTable: "_OperationClaims",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__UserOperationClaims__Users_UserId",
                        column: x => x.UserId,
                        principalTable: "_Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "_UserWhiteCurrencies",
                columns: table => new
                {
                    CurrencyId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__UserWhiteCurrencies", x => new { x.UserId, x.CurrencyId });
                    table.ForeignKey(
                        name: "FK__UserWhiteCurrencies__Currencies_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "_Currencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__UserWhiteCurrencies__Users_UserId",
                        column: x => x.UserId,
                        principalTable: "_Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "_CompletedProcesses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CurrencyId = table.Column<int>(type: "int", maxLength: 20, nullable: false),
                    BoughtPrice = table.Column<decimal>(type: "money", nullable: false),
                    SoldPrice = table.Column<decimal>(type: "money", nullable: false),
                    SoldDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__CompletedProcesses", x => x.Id);
                    table.ForeignKey(
                        name: "FK__CompletedProcesses__Currencies_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "_Currencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK__CompletedProcesses__Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "_Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK__CompletedProcesses__Users_UserId",
                        column: x => x.UserId,
                        principalTable: "_Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "_CurrentProcesses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BoughtDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SpendRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    TimeId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CurrencyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__CurrentProcesses", x => x.Id);
                    table.ForeignKey(
                        name: "FK__CurrentProcesses__Currencies_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "_Currencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__CurrentProcesses__Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "_Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK__CurrentProcesses__Times_TimeId",
                        column: x => x.TimeId,
                        principalTable: "_Times",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK__CurrentProcesses__Users_UserId",
                        column: x => x.UserId,
                        principalTable: "_Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "_OrderTimes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TimeId = table.Column<int>(type: "int", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__OrderTimes", x => x.Id);
                    table.ForeignKey(
                        name: "FK__OrderTimes__Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "_Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__OrderTimes__Times_TimeId",
                        column: x => x.TimeId,
                        principalTable: "_Times",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX__CompletedProcesses_CurrencyId",
                table: "_CompletedProcesses",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX__CompletedProcesses_OrderId",
                table: "_CompletedProcesses",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX__CompletedProcesses_UserId",
                table: "_CompletedProcesses",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX__CurrentProcesses_CurrencyId",
                table: "_CurrentProcesses",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX__CurrentProcesses_OrderId",
                table: "_CurrentProcesses",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX__CurrentProcesses_TimeId",
                table: "_CurrentProcesses",
                column: "TimeId");

            migrationBuilder.CreateIndex(
                name: "IX__CurrentProcesses_UserId",
                table: "_CurrentProcesses",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX__Orders_UserId",
                table: "_Orders",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX__OrderTimes_OrderId",
                table: "_OrderTimes",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX__OrderTimes_TimeId",
                table: "_OrderTimes",
                column: "TimeId");

            migrationBuilder.CreateIndex(
                name: "IX__UserOperationClaims_OperationClaimId",
                table: "_UserOperationClaims",
                column: "OperationClaimId");

            migrationBuilder.CreateIndex(
                name: "IX__UserOperationClaims_UserId",
                table: "_UserOperationClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX__UserWhiteCurrencies_CurrencyId",
                table: "_UserWhiteCurrencies",
                column: "CurrencyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "_CompletedProcesses");

            migrationBuilder.DropTable(
                name: "_CurrentProcesses");

            migrationBuilder.DropTable(
                name: "_OrderTimes");

            migrationBuilder.DropTable(
                name: "_UserApis");

            migrationBuilder.DropTable(
                name: "_UserOperationClaims");

            migrationBuilder.DropTable(
                name: "_UserWhiteCurrencies");

            migrationBuilder.DropTable(
                name: "_Orders");

            migrationBuilder.DropTable(
                name: "_Times");

            migrationBuilder.DropTable(
                name: "_OperationClaims");

            migrationBuilder.DropTable(
                name: "_Currencies");

            migrationBuilder.DropTable(
                name: "_Users");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MailAutomation.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Expense",
                columns: table => new
                {
                    ExpenseID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CostCentre = table.Column<string>(type: "varchar(15)", nullable: false),
                    SalesTaxAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TaxableAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NetAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PaymentMethod = table.Column<string>(type: "varchar(50)", nullable: false),
                    ExtractedContent = table.Column<string>(type: "varchar(500)", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Expense", x => x.ExpenseID);
                });

            migrationBuilder.CreateTable(
                name: "ProcessedMailInfo",
                columns: table => new
                {
                    ProcessedMailInfoID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MessageID = table.Column<string>(type: "varchar(100)", nullable: false),
                    MessageDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SenderName = table.Column<string>(type: "varchar(50)", nullable: false),
                    SenderEmailAddress = table.Column<string>(type: "varchar(100)", nullable: false),
                    Subject = table.Column<string>(type: "varchar(100)", nullable: false),
                    Body = table.Column<string>(type: "varchar(1000)", nullable: false),
                    ExpenseID = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcessedMailInfo", x => x.ProcessedMailInfoID);
                    table.ForeignKey(
                        name: "FK_ProcessedMailInfo_Expense_ExpenseID",
                        column: x => x.ExpenseID,
                        principalTable: "Expense",
                        principalColumn: "ExpenseID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProcessedMailInfo_ExpenseID",
                table: "ProcessedMailInfo",
                column: "ExpenseID",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProcessedMailInfo");

            migrationBuilder.DropTable(
                name: "Expense");
        }
    }
}

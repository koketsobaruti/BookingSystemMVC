using Microsoft.EntityFrameworkCore.Migrations;

namespace BookingSystem_Prototype_MVC.Migrations
{
    public partial class addBusinessServicesToDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BusinessServices",
                columns: table => new
                {
                    ServiceID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ServiceName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ServiceDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessServices", x => x.ServiceID);
                    table.ForeignKey(
                        name: "FK_BusinessServices_Business_ID",
                        column: x => x.ID,
                        principalTable: "Business",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BusinessServices_ID",
                table: "BusinessServices",
                column: "ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BusinessServices");
        }
    }
}

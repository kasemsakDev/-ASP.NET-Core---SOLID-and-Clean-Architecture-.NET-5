using Microsoft.EntityFrameworkCore.Migrations;

namespace HR.LeaveManagement1.Persistence.Migrations
{
    public partial class AddRequestingEmployeeIdToLeaveRequest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RequestingEmployeeId",
                table: "LeaveRequests",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RequestingEmployeeId",
                table: "LeaveRequests");
        }
    }
}

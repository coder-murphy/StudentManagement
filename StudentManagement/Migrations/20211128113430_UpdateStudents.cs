using Microsoft.EntityFrameworkCore.Migrations;

namespace StudentManagement.Migrations
{
    public partial class UpdateStudents : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Icon",
                table: "Students",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "Email", "Icon", "Major", "Name" },
                values: new object[] { 1, "123456@sina.com", null, 3, "张三" });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "Email", "Icon", "Major", "Name" },
                values: new object[] { 2, "512351@gmail.com", null, 1, "李四" });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "Email", "Icon", "Major", "Name" },
                values: new object[] { 3, "zsix33333@yahoo.com", null, 2, "赵六" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DropColumn(
                name: "Icon",
                table: "Students");
        }
    }
}

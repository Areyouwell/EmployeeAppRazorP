using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RazorPages.Services.Migrations
{
    /// <inheritdoc />
    public partial class AddNewEmployee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string procedure = @"Create procedure spAddNewEmployee
                                @Name nvarchar(100),
                                @Email nvarchar(100),
                                @PhotoPath nvarchar(100),
                                @Dept int
                                as
                                Begin
                                    INSERT INTO Employees (Name, Email, PhotoPath, Department)
                                    VALUES (@Name, @Email, @PhotoPath, @Dept)
                                END";
            migrationBuilder.Sql(procedure);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            string procedure = @"Drop procedure spAddNewEmployee";
            migrationBuilder.Sql(procedure);
        }
    }
}

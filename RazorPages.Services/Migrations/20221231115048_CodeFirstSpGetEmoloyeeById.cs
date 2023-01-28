using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RazorPages.Services.Migrations
{
    /// <inheritdoc />
    public partial class CodeFirstSpGetEmoloyeeById : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string procedure = @"Create procedure CodeFirstSpGetEmoloyeeById
                                    @Id int
                                    as
                                    Begin
                                        Select * from Employees
                                        where Id = @Id
                                    End";

            migrationBuilder.Sql(procedure);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            string procedure = @"Drop procedure CodeFirstSpGetEmoloyeeById";

            migrationBuilder.Sql(procedure);
        }
    }
}

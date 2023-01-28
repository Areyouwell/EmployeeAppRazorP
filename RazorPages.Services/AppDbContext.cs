using Microsoft.EntityFrameworkCore;
using RazorPages.Models;

namespace RazorPages.Services
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Employee> Empoyees { get; set; }
    }
}

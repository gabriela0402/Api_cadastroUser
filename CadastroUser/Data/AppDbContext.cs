using CadastroUser.Models;
using CadastroUser.Models.User;
using Microsoft.EntityFrameworkCore;

namespace CadastroUser.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<UserModel> Usuarios { get; set; }
    }
}

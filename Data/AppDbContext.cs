using Microsoft.EntityFrameworkCore;
using tasks_api.Models;

namespace tasks_api.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {

        public DbSet<Usuario> Users { get; set; }
        public DbSet<Tarefa> Tarefas { get; set; }
    }
}
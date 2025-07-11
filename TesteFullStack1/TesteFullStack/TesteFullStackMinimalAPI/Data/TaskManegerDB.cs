using Microsoft.EntityFrameworkCore;

namespace TesteFullStackMinimalAPI.Data
{
    public class TaskManegerDB : DbContext 
    {
        public TaskManegerDB(DbContextOptions<TaskManegerDB> options) 
            : base(options)
        {
        }
        public DbSet<Models.Tarefa> Tarefas { get; set; } = null!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Models.Tarefa>().HasKey(t => t.Id);
            modelBuilder.Entity<Models.Tarefa>().Property(t => t.Nome).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<Models.Tarefa>().Property(t => t.Detalhes).IsRequired().HasMaxLength(500);
            modelBuilder.Entity<Models.Tarefa>().Property(t => t.Concluida).IsRequired();
            modelBuilder.Entity<Models.Tarefa>().Property(t => t.DataCadastro).IsRequired();
            modelBuilder.Entity<Models.Tarefa>().Property(t => t.DataConclusao).IsRequired(false);
        }
    }
}

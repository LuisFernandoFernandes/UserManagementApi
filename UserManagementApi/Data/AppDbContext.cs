using Microsoft.EntityFrameworkCore;
using UserManagementApi.Models;

namespace UserManagementApi.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Grupo> Grupo { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>()
                .HasOne(u => u.Grupo)
                .WithMany()
                .HasForeignKey(u => u.IdGrupo);

            base.OnModelCreating(modelBuilder);
        }

        public void InitializeAdminUser()
        {
            var adminGrupo = new Grupo
            {
                Descricao = "ADMINISTRADOR",
                Administrador = true,
                DataCadastro = DateTime.Now,
                DataAlteracao = DateTime.Now
            };

            var adminUsuario = new Usuario
            {
                Nome = "ADMIN",
                Senha = "ADMIN", // Lembre-se de criptografar a senha
                CPF = "12345678900", // Ou outro CPF válido
                Grupo = adminGrupo,
                DataCadastro = DateTime.Now,
                DataAlteracao = DateTime.Now
            };

            Grupo.Add(adminGrupo);
            Usuario.Add(adminUsuario);

            SaveChanges();
        }
    }
}

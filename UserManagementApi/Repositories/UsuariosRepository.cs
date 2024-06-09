using Microsoft.EntityFrameworkCore;
using UserManagementApi.Data;
using UserManagementApi.Interfaces;
using UserManagementApi.Models;

namespace UserManagementApi.Repositories
{
    public class UsuariosRepository : GenericRepository<Usuario>, IUsuariosRepository
    {
        private readonly AppDbContext _context;
        public UsuariosRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> AddUsuarioAsync(Usuario usuario)
        {
            var jaCadastrado = await _context.Usuario.AnyAsync(u => u.Nome == usuario.Nome);
            if (!jaCadastrado)
            {
                await _context.Usuario.AddAsync(usuario);
                await _context.SaveChangesAsync();
            }
            return jaCadastrado;
        }

        public async Task<bool> UpdateUsuarioAsync(Usuario usuario, Usuario oldUsuario)
        {
            var jaCadastrado = await _context.Usuario.AnyAsync(u => u.Nome == usuario.Nome && u.Id != usuario.Id);
            if (!jaCadastrado)
            {
                oldUsuario.Nome = usuario.Nome;
                oldUsuario.Senha = usuario.Senha;
                oldUsuario.CPF = usuario.CPF;
                oldUsuario.IdGrupo = usuario.IdGrupo;

                _context.Usuario.Update(oldUsuario);
                await _context.SaveChangesAsync();
            }

            return jaCadastrado;
        }


        public async Task<Grupo?> GetGrupoByUsuario(Usuario usuario)
        {
            var grupo = await _context.Grupo.FirstOrDefaultAsync(g => g.Id == usuario.IdGrupo);
            return grupo;
        }

        public async Task<Usuario?> GetUserByUsername(string username)
        {
            var usuario = await _context.Usuario.FirstOrDefaultAsync(u => u.Nome == username);
            return usuario;
        }
    }
}

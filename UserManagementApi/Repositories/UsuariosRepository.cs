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

        public async Task<Usuario?> GetUserByUsername(string username)
        {
            var usuario = await _context.Usuario.FirstOrDefaultAsync(u => u.Nome == username);
            return usuario;
        }
    }
}

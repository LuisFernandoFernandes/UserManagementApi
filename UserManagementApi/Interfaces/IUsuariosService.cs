using UserManagementApi.Models;

namespace UserManagementApi.Interfaces
{
    public interface IUsuariosService : IGenericService<Usuario>
    {
        Task<Usuario?> GetUserByUsername(string username);
        bool VerifyPassword(string password, string passwordHash);
        Task<string> GenerateJwtToken(Usuario usuario);
        Task<Usuario> AddUsuarioAsync(Usuario usuario);
        Task<Usuario> UpdateUsuarioAsync(Usuario usuario);
    }
}

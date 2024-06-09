using UserManagementApi.Models;

namespace UserManagementApi.Interfaces
{
    public interface IUsuariosService : IGenericService<Usuario>
    {
        Task<Usuario?> GetUserByUsername(string username);
        bool VerifyPassword(string password, string passwordHash);
        string GenerateJwtToken(Usuario usuario);
    }
}

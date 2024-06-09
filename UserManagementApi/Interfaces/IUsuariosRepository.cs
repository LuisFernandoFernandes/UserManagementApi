using UserManagementApi.Interface;
using UserManagementApi.Models;

namespace UserManagementApi.Interfaces
{
    public interface IUsuariosRepository : IGenericRepository<Usuario>
    {
        Task<Usuario?> GetUserByUsername(string username);
        Task<Grupo?> GetGrupoByUsuario(Usuario usuario);
        Task<bool> AddUsuarioAsync(Usuario usuario);
        Task<bool> UpdateUsuarioAsync(Usuario usuario, Usuario oldUsuario);
    }

}

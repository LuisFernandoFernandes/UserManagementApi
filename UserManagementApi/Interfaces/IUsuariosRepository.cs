using UserManagementApi.Interface;
using UserManagementApi.Models;

namespace UserManagementApi.Interfaces
{
    public interface IUsuariosRepository : IGenericRepository<Usuario>
    {
        Task<Usuario?> GetUserByUsername(string username);
    }

}

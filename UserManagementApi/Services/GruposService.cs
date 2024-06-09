using UserManagementApi.Interface;
using UserManagementApi.Interfaces;
using UserManagementApi.Models;

namespace UserManagementApi.Services
{
    public class GruposService : GenericService<Grupo>, IGruposService
    {
        public GruposService(IGenericRepository<Grupo> repository) : base(repository)
        {
        }
    }
}

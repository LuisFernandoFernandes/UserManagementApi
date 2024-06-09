using UserManagementApi.Data;
using UserManagementApi.Interfaces;
using UserManagementApi.Models;

namespace UserManagementApi.Repositories
{
    public class GruposRepository : GenericRepository<Grupo>, IGruposRepository
    {
        public GruposRepository(AppDbContext context) : base(context) { }
    }
}

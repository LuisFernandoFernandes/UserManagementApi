using Microsoft.AspNetCore.Mvc;
using UserManagementApi.Interfaces;
using UserManagementApi.Models;

namespace UserManagementApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GruposController : GenericController<Grupo>
    {
        public GruposController(IGenericService<Grupo> service) : base(service)
        {
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserManagementApi.DTO;
using UserManagementApi.Interfaces;
using UserManagementApi.Models;
using UserManagementApi.Repositories;

namespace UserManagementApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : GenericController<Usuario>
    {
        private readonly IUsuariosService _usuariosService;

        public UsuariosController(IGenericService<Usuario> service, IUsuariosService usuariosService) : base(service)
        {
            _usuariosService = usuariosService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            var user = await _usuariosService.GetUserByUsername(loginDTO.Username);

            if (user == null || !_usuariosService.VerifyPassword(loginDTO.Password, user.Senha))
            {
                return Unauthorized();
            }

            var token = _usuariosService.GenerateJwtToken(user);

            return Ok(new { token });
        }
    }
}

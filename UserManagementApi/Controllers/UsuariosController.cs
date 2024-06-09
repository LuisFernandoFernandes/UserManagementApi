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

        [Authorize(Policy = "ApenasAdministrador")]
        [HttpPost("creatwithvalidation")]
        public async Task<IActionResult> CreateUsuario([FromBody] Usuario usuario)
        {
            try
            {
                var createdUsuario = await _usuariosService.AddUsuarioAsync(usuario);
                return CreatedAtAction(nameof(GetById), new { id = createdUsuario.Id }, createdUsuario);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [Authorize(Policy = "ApenasAdministrador")]
        [HttpPut("updatewithvalidation/{id}")]
        public async Task<IActionResult> UpdateUsuario(int id, [FromBody] Usuario usuario)
        {
            if (id != usuario.Id)
            {
                return BadRequest(new { message = "ID do usuário inválido." });
            }

            try
            {
                var updatedUsuario = await _usuariosService.UpdateUsuarioAsync(usuario);
                return Ok(updatedUsuario);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}

using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using UserManagementApi.Interface;
using UserManagementApi.Interfaces;
using UserManagementApi.Models;
using UserManagementApi.Repositories;

namespace UserManagementApi.Services
{
    public class UsuariosService : GenericService<Usuario>, IUsuariosService
    {
        private readonly IUsuariosRepository _repository;

        public UsuariosService(IUsuariosRepository repository) : base(repository)
        {
            _repository = repository;
        }

        public async Task<Usuario?> GetUserByUsername(string username)
        {
            return await _repository.GetUserByUsername(username);
        }

        public bool VerifyPassword(string password, string passwordHash)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // Calcula o hash da senha fornecida
                byte[] inputBytes = Encoding.UTF8.GetBytes(password);
                byte[] hashedBytes = sha256Hash.ComputeHash(inputBytes);
                string hashedPassword = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();

                // Compara o hash da senha fornecida com o hash armazenado no banco de dados
                return hashedPassword == passwordHash;
            }
        }


        public async Task<string> GenerateJwtToken(Usuario usuario)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                new Claim(ClaimTypes.Name, usuario.Nome),
            };

            var grupo = await _repository.GetGrupoByUsuario(usuario);

            // Verifica se o usuário pertence a um grupo administrador e inclui a claim correspondente
            if (grupo != null && grupo.Administrador)
            {
                claims.Add(new Claim("Administrador", "true"));
            }
            else
            {
                claims.Add(new Claim("Administrador", "false"));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("YsTzB9Jy0tqgNzApAAAAAAAAAAAAYsTzB9Jy0tqgNzApAAAAAAAAAAAAYsTzB9Jy0tqgNzApAAAAAAAAAAAAYsTzB9Jy0tqgNzApAAAAAAAAAAAA"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddHours(12),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<Usuario> AddUsuarioAsync(Usuario usuario)
        {
            if (await _repository.AddUsuarioAsync(usuario))
            {
                throw new Exception("Nome de usuário já existe.");
            }

            return usuario;
        }

        public async Task<Usuario> UpdateUsuarioAsync(Usuario usuario)
        {
            var oldUsuario = await _repository.GetById(usuario.Id);
            if (oldUsuario == null)
            {
                throw new Exception("Usuário não encontrado.");
            }

            if (await _repository.UpdateUsuarioAsync(usuario, oldUsuario))
            {
                throw new Exception("Nome de usuário já existe.");
            }

            return usuario;
        }
    }
}

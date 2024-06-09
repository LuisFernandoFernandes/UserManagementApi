using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using UserManagementApi.Interface;
using UserManagementApi.Interfaces;
using UserManagementApi.Models;

namespace UserManagementApi.Services
{
    public class UsuariosService : GenericService<Usuario>, IUsuariosService
    {
        private readonly IUsuariosRepository _repository;

        // Correção na injeção de dependência
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


        public string GenerateJwtToken(Usuario usuario)
        {
            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
        new Claim(ClaimTypes.Name, usuario.Nome),
    };

            // Verifica se o usuário pertence a um grupo administrador e inclui a claim correspondente
            if (usuario.Grupo.Administrador)
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
    }
}

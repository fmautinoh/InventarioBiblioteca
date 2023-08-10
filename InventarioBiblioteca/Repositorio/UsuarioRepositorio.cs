using InventarioBiblioteca.Modelos;
using InventarioBiblioteca.Modelos.ModelDto;
using InventarioBiblioteca.Repositorio.IRepositorio;
using Isopoh.Cryptography.Argon2;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NuGet.Common;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace InventarioBiblioteca.Repositorio
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly DatabaseContext _databaseContext;
        private string secretkey;

        public UsuarioRepositorio(DatabaseContext databaseContext, IConfiguration configuration)
        {
            _databaseContext = databaseContext;
            secretkey = configuration.GetValue<string>("ApiSettings:Secret");
        }





        public async Task<LoginResponseDto> Login(UsuarioDto LgDto)
        {

            var contraseña = (LgDto.Pwsd);
            var usuario = await _databaseContext.Usuarios.FirstOrDefaultAsync(u => u.Usu.ToLower() == LgDto.Usu.ToLower());
            var tokenHandler= new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretkey);
            SecurityToken? Token = null;
            if (usuario == null)
            {
                return new LoginResponseDto()
                {
                    Token = "",
                    Usuario = null
                };
            }
            var passwordHash = Argon2.Hash(usuario.Pwsd);
            if (Argon2.Verify(passwordHash, contraseña))
            {
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim(ClaimTypes.NameIdentifier, usuario.Usuarioid.ToString()),
                    new Claim(ClaimTypes.Name,usuario.Usu.ToString()),
                    new Claim(ClaimTypes.Role,usuario.Tipousuario.Tipousuario1.ToString())

                    }),
                    Expires = DateTime.UtcNow.AddHours(8),
                    SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                    Token = token;
            }
            LoginResponseDto loginResponseDto = new()
            {
                Token = tokenHandler.WriteToken(Token),
                Usuario = usuario
            };
            return loginResponseDto;

        }
    }
}

using AutoMapper;
using InventarioBiblioteca.Modelos;
using InventarioBiblioteca.Modelos.ModelDto;
using InventarioBiblioteca.Repositorio.IRepositorio;
using Isopoh.Cryptography.Argon2;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NuGet.Common;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace InventarioBiblioteca.Repositorio
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly DatabaseContext _databaseContext;
        private readonly IMapper _mapper;
        private string secretkey;

        public UsuarioRepositorio(DatabaseContext databaseContext, IConfiguration configuration, IMapper mapper)
        {
            _databaseContext = databaseContext;
            _mapper = mapper;
            secretkey = configuration.GetValue<string>("ApiSettings:Secret");
        }


        public async Task<Usuario> Register(UsuarioCreatedDto modelUsuario)
        {
            var exist = _databaseContext.Usuarios.FirstOrDefaultAsync(e => e.Usu.ToLower() == modelUsuario.Usu.ToLower());
            if (exist==null)
            {
                return null;
            }
            var passwordHash = Argon2.Hash(modelUsuario.Pwsd);
            Usuario modelUsuarioCrt = new() { 
            Usu = modelUsuario.Usu,
            Pwsd = passwordHash,
            Tipousuarioid =modelUsuario.Tipousuarioid
            };
            await _databaseContext.Usuarios.AddAsync(modelUsuarioCrt);
            await _databaseContext.SaveChangesAsync();
            return modelUsuarioCrt;
        }


        public async Task<LoginResponseDto> Login(UsuarioDto LgDto)
        {
            var usuario = await _databaseContext.Usuarios.FirstOrDefaultAsync(u => u.Usu.ToLower() == LgDto.Usu.ToLower());
            LoginResponseDto envio = new LoginResponseDto();
            if (usuario == null)
            {
                return new LoginResponseDto()
                {
                    Token = "",
                    Usuario = null
                };
            }

            if (Argon2.Verify(usuario.Pwsd, LgDto.Pwsd))
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(secretkey);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim(ClaimTypes.NameIdentifier, usuario.Usuarioid.ToString()),
                    new Claim(ClaimTypes.Name,usuario.Usu.ToString()),
                    new Claim(ClaimTypes.Sid,usuario.Tipousuarioid.ToString())

                    }),
                    Expires = DateTime.UtcNow.AddHours(8),
                    SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                    Issuer = "InventarioBiblioteca",
                    Audience = "bookmanager-main"
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                  envio = new LoginResponseDto() {
                  Token= tokenHandler.WriteToken(token),
                  Usuario = usuario
                };
            }
            
            return envio;

        }
    }
}

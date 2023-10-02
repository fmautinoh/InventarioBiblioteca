using InventarioBiblioteca.Models;
using InventarioBiblioteca.Models.ModelsDto;

namespace InventarioBiblioteca.Repositorio.IRepositorio
{
    public interface IUsuarioRepositorio
    {
        Task<LoginResponseDto> Login(UsuarioDto LgDto);
        Task<Usuario> Register(UsuarioCreatedDto UsuCrear);
    }
}

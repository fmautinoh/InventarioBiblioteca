using InventarioBiblioteca.Modelos;
using InventarioBiblioteca.Modelos.ModelDto;

namespace InventarioBiblioteca.Repositorio.IRepositorio
{
    public interface IUsuarioRepositorio
    {
        Task<LoginResponseDto> Login(UsuarioDto LgDto);
        Task<Usuario> Register(UsuarioCreatedDto UsuCrear);
    }
}

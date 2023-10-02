using InventarioBiblioteca.Models;

namespace InventarioBiblioteca.Repositorio.IRepositorio
{
    public interface ILibroxAutorRepositorio : IRepositorio<LibrosAutore>
    {
        Task<LibrosAutore> Actualizar(LibrosAutore entidad);
    }
}

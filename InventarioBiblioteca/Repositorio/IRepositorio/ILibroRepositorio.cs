using InventarioBiblioteca.Models;

namespace InventarioBiblioteca.Repositorio.IRepositorio
{
    public interface ILibroRepositorio : IRepositorio<Libro>
    {
        Task<Libro> Actualizar(Libro entidad);

    }
}

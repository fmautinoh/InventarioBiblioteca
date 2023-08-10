using InventarioBiblioteca.Modelos;

namespace InventarioBiblioteca.Repositorio.IRepositorio
{
    public interface ILibroRepositorio : IRepositorio<Libro>
    {
        Task<Libro> Actualizar(Libro entidad);

    }
}

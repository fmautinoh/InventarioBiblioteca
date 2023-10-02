using InventarioBiblioteca.Models;

namespace InventarioBiblioteca.Repositorio.IRepositorio
{
    public interface IInventarioRepositorio : IRepositorio<InventarioLibro>
    {
        Task<InventarioLibro> Actualizar(InventarioLibro entidad);
    }
}

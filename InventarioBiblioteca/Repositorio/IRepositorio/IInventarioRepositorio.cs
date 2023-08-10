using InventarioBiblioteca.Modelos;

namespace InventarioBiblioteca.Repositorio.IRepositorio
{
    public interface IInventarioRepositorio : IRepositorio<Inventariolibro>
    {
        Task<Inventariolibro> Actualizar(Inventariolibro entidad);
    }
}

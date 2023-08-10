using InventarioBiblioteca.Modelos;

namespace InventarioBiblioteca.Repositorio.IRepositorio
{
    public interface ILibroxAutorRepositorio : IRepositorio<Librosautore>
    {
        Task<Librosautore> Actualizar(Librosautore entidad);
    }
}

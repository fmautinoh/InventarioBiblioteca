using InventarioBiblioteca.Modelos;

namespace InventarioBiblioteca.Repositorio.IRepositorio
{
    public interface IAutorRepositorio : IRepositorio<Autore>
    {
        Task<Autore> Actualizar(Autore entidad);
    }
}

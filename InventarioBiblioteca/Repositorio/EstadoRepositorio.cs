using InventarioBiblioteca.Models;
using InventarioBiblioteca.Repositorio.IRepositorio;

namespace InventarioBiblioteca.Repositorio
{
    public class EstadoRepositorio : ReaderRepositorio<EstadoConservacion>, IEstadoRepositorio
    {
        private readonly DatabaseContext _context;

        public EstadoRepositorio(DatabaseContext db) : base(db)
        {
            _context = db;
        }
    }
}

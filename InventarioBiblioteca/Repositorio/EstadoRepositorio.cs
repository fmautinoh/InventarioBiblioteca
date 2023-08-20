using InventarioBiblioteca.Modelos;
using InventarioBiblioteca.Repositorio.IRepositorio;

namespace InventarioBiblioteca.Repositorio
{
    public class EstadoRepositorio : ReaderRepositorio<Estadoconservacion>, IEstadoRepositorio
    {
        private readonly DatabaseContext _context;

        public EstadoRepositorio(DatabaseContext db) : base(db)
        {
            _context = db;
        }
    }
}

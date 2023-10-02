using InventarioBiblioteca.Models;
using InventarioBiblioteca.Repositorio.IRepositorio;

namespace InventarioBiblioteca.Repositorio
{
    public class TipoLibroRepositorio : ReaderRepositorio<TipoLibro>, ITipoLibroRepositorio
    {
        private readonly DatabaseContext _context;

        public TipoLibroRepositorio(DatabaseContext db) : base(db)
        {
            _context = db;
        }
    }
}

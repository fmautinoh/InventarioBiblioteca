using InventarioBiblioteca.Modelos;
using InventarioBiblioteca.Repositorio.IRepositorio;

namespace InventarioBiblioteca.Repositorio
{
    public class TipoLibroRepositorio : ReaderRepositorio<Tipolibro>, ITipoLibroRepositorio
    {
        private readonly DatabaseContext _context;

        public TipoLibroRepositorio(DatabaseContext db) : base(db)
        {
            _context = db;
        }
    }
}

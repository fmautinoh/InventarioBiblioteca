using InventarioBiblioteca.Models;
using InventarioBiblioteca.Repositorio.IRepositorio;

namespace InventarioBiblioteca.Repositorio
{
    public class TipoAutorRepositorio : ReaderRepositorio<TipoAutor>, ITipoAutorRepositorio
    {
        private readonly DatabaseContext _context;

        public TipoAutorRepositorio(DatabaseContext db) : base(db)
        {
            _context = db;
        }
    }
}

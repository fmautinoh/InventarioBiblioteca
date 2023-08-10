using InventarioBiblioteca.Modelos;
using InventarioBiblioteca.Repositorio.IRepositorio;

namespace InventarioBiblioteca.Repositorio
{
    public class TipoAutorRepositorio : ReaderRepositorio<Tipoautor>, ITipoAutorRepositorio
    {
        private readonly DatabaseContext _context;

        public TipoAutorRepositorio(DatabaseContext db) : base(db)
        {
            _context = db;
        }
    }
}

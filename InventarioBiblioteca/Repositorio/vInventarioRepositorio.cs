using InventarioBiblioteca.Models;
using InventarioBiblioteca.Repositorio.IRepositorio;

namespace InventarioBiblioteca.Repositorio
{
    public class vInventarioRepositorio : ReaderRepositorio<VInventario>, IvInventarioRepositorio
    {
        private readonly DatabaseContext _context;

        public vInventarioRepositorio(DatabaseContext db) : base(db)
        {
            _context = db;
        }
    }
}

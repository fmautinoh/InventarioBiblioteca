using InventarioBiblioteca.Models;
using InventarioBiblioteca.Repositorio.IRepositorio;

namespace InventarioBiblioteca.Repositorio
{
    public class AutenticidadRepositorio : ReaderRepositorio<Autenticidad>, IAutenticidadRepositorio
    {
        private readonly DatabaseContext _context;

        public AutenticidadRepositorio(DatabaseContext db) : base(db)
        {
            _context = db;
        }
    }
}

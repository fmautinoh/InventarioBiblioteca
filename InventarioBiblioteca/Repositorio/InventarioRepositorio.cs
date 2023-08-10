using InventarioBiblioteca.Modelos;
using InventarioBiblioteca.Repositorio.IRepositorio;

namespace InventarioBiblioteca.Repositorio
{
    public class InventarioRepositorio : Repositorio<Inventariolibro>, IInventarioRepositorio
    {
        private readonly DatabaseContext _context;

        public InventarioRepositorio(DatabaseContext db) : base(db)
        {
            _context = db;
        }

        public async Task<Inventariolibro> Actualizar(Inventariolibro entidad)
        {
            _context.Inventariolibros.Update(entidad);
            await _context.SaveChangesAsync();
            return entidad;
        }
    }
}

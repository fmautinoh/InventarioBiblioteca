using InventarioBiblioteca.Modelos;
using InventarioBiblioteca.Repositorio.IRepositorio;
using Microsoft.EntityFrameworkCore;

namespace InventarioBiblioteca.Repositorio
{
    public class LibroxAutorRepositorio : Repositorio<Librosautore>, ILibroxAutorRepositorio
    {
        private readonly DatabaseContext _context;

        public LibroxAutorRepositorio(DatabaseContext db) : base(db)
        {
            _context = db;
        }

        public async Task<Librosautore> Actualizar(Librosautore entidad)
        {
            _context.Librosautores.Update(entidad);
            await _context.SaveChangesAsync();
            return entidad;
        }
    }
}

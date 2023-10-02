using InventarioBiblioteca.Models;
using InventarioBiblioteca.Repositorio.IRepositorio;
using Microsoft.EntityFrameworkCore;

namespace InventarioBiblioteca.Repositorio
{
    public class LibroxAutorRepositorio : Repositorio<LibrosAutore>, ILibroxAutorRepositorio
    {
        private readonly DatabaseContext _context;

        public LibroxAutorRepositorio(DatabaseContext db) : base(db)
        {
            _context = db;
        }

        public async Task<LibrosAutore> Actualizar(LibrosAutore entidad)
        {
            _context.LibrosAutores.Update(entidad);
            await _context.SaveChangesAsync();
            return entidad;
        }
    }
}

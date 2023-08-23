using InventarioBiblioteca.Modelos;
using InventarioBiblioteca.Repositorio.IRepositorio;

namespace InventarioBiblioteca.Repositorio;

public class vAutorRepositorio : ReaderRepositorio<VAutor>, IvautorRepositorio
{
    private readonly DatabaseContext _context;

    public vAutorRepositorio(DatabaseContext db) : base(db)
    {
        _context = db;
    }
}
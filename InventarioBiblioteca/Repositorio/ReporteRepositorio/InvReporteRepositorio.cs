using InventarioBiblioteca.Modelos;
using InventarioBiblioteca.Repositorio.IRepositorio.IReporteRepositorio;

namespace InventarioBiblioteca.Repositorio.ReporteRepositorio
{
    public class InvReporteRepositorio : ReaderRepositorio<VInvReporte>, IRepoteInventarioRepositorio
    {
        private readonly DatabaseContext _context;

        public InvReporteRepositorio(DatabaseContext db) : base(db)
        {
            _context = db;
        }
    }
}

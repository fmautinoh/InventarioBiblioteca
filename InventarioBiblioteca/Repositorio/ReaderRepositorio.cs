using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Linq;
using InventarioBiblioteca.Repositorio.IRepositorio;
using InventarioBiblioteca.Modelos;

namespace InventarioBiblioteca.Repositorio
{
    public class ReaderRepositorio<T> : IReaderRepositorio<T> where T : class
    {
        private readonly DatabaseContext _context;
        internal DbSet<T> dbSet;
        public ReaderRepositorio(DatabaseContext db)
        {
            _context = db;
            dbSet = _context.Set<T>();
        }
        public async Task<T> Listar(Expression<Func<T, bool>>? filtro = null, bool tracked = true)
        {
            IQueryable<T> query = dbSet;
            if (!tracked)
            {
                query = query.AsNoTracking();
            }
            if (filtro != null)
            {
                query = query.Where(filtro);
            }
            return await query.FirstOrDefaultAsync();
        }

        public async Task<List<T>> ListObjetos(Expression<Func<T, bool>>? filtro = null)
        {
            IQueryable<T> query = dbSet;
            if (filtro != null)
            {
                query = query.Where(filtro);
            }
            return await query.ToListAsync();
        }
    }
}

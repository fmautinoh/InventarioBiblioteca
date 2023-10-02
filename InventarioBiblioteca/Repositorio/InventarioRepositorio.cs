﻿using InventarioBiblioteca.Models;
using InventarioBiblioteca.Repositorio.IRepositorio;

namespace InventarioBiblioteca.Repositorio
{
    public class InventarioRepositorio : Repositorio<InventarioLibro>, IInventarioRepositorio
    {
        private readonly DatabaseContext _context;

        public InventarioRepositorio(DatabaseContext db) : base(db)
        {
            _context = db;
        }

        public async Task<InventarioLibro> Actualizar(InventarioLibro entidad)
        {
            _context.InventarioLibros.Update(entidad);
            await _context.SaveChangesAsync();
            return entidad;
        }
    }
}

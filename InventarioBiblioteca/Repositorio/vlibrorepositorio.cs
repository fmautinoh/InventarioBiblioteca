using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InventarioBiblioteca.Modelos;
using InventarioBiblioteca.Modelos.ModelDto;
using InventarioBiblioteca.Repositorio.IRepositorio;
using Microsoft.EntityFrameworkCore;

namespace InventarioBiblioteca.Repositorio
{
    public class vlibrorepositorio : ReaderRepositorio<VLibro>, Ivlibrorepositorio
    {
        private readonly DatabaseContext _context;

        public vlibrorepositorio(DatabaseContext db) : base(db)
        {
            _context = db;
        }

        // Implement the ListLibrosConAutores method
        public async Task<List<VLibro>> ListLibrosConAutores()
        {
            // Fetch the grouped books without including the authors
            var librosGrouped = await _context.VLibros
                .GroupBy(libro => libro.Libroid)
                .Select(group => new VLibro
                {
                    Libroid = group.Key,
                    Nombrelib = group.First().Nombrelib,
                    Tipolibroid = group.First().Tipolibroid,
                    Tipolibro = group.First().Tipolibro,
                    Edicion = group.First().Edicion,
                    Año = group.First().Año,
                    Editorial = group.First().Editorial,
                })
                .ToListAsync();

            // Fetch the authors separately
            var autores = await _context.VLibros
                .Select(libro => new { libro.Libroid, libro.Autorid, libro.Nombreautor })
                .Distinct()
                .ToListAsync();

            // Map the authors to the corresponding books
            foreach (var libro in librosGrouped)
            {
                libro.AutoresIds = autores
                    .Where(a => a.Libroid == libro.Libroid && a.Autorid != 0) // Exclude entries with autorId = 0
                    .Select(a => new AutorDtosList { AutorId = (int)a.Autorid, NombreAutor = a.Nombreautor })
                    .ToList();
            }

            return librosGrouped;
        }
        
        public async Task<VLibro> GetLibroConAutoresPorLibroid(int libroid)
        {
            // Fetch the grouped book without including the authors
            var libroGrouped = await _context.VLibros
                .Where(libro => libro.Libroid == libroid)
                .Select(group => new VLibro
                {
                    Libroid = group.Libroid,
                    Nombrelib = group.Nombrelib,
                    Tipolibroid = group.Tipolibroid,
                    Tipolibro = group.Tipolibro,
                    Edicion = group.Edicion,
                    Año = group.Año,
                    Editorial = group.Editorial,
                    AutoresIds = new List<AutorDtosList>() // Initialize an empty list for AutoresIds
                })
                .FirstOrDefaultAsync();

            if (libroGrouped != null)
            {
                // Fetch the authors for the specified libro
                var autores = await _context.VLibros
                    .Where(libro => libro.Libroid == libroid && libro.Autorid != 0) // Exclude entries with autorId = 0
                    .Select(a => new AutorDtosList { AutorId = (int)a.Autorid, NombreAutor = a.Nombreautor })
                    .ToListAsync();

                libroGrouped.AutoresIds.AddRange(autores); // Add authors to the AutoresIds list
            }

            return libroGrouped;
        }

    }
}

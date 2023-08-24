using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InventarioBiblioteca.Modelos;

namespace InventarioBiblioteca.Repositorio.IRepositorio
{
    public interface Ivlibrorepositorio : IReaderRepositorio<VLibro>
    {
        // Add a new method to fetch books with multiple authors
        Task<List<VLibro>> ListLibrosConAutores();
        Task<VLibro> GetLibroConAutoresPorLibroid(int libroid);
    }
}

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace InventarioBiblioteca.Modelos.ModelDto
{
    public class LibroAutorCreatedDto
    {
        [JsonRequired]
        [Required]
        public int LibroId { get; set; }
        [JsonRequired]
        [Required]
        public int AutorId { get; set; }
    }
}

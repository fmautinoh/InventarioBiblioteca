using InventarioBiblioteca.Modelos;
using InventarioBiblioteca.Modelos.ModelDto;
using AutoMapper;

namespace InventarioBiblioteca
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Autore, AutorDto>().ReverseMap();
            CreateMap<Autore, AutorCreatedDto>().ReverseMap();
            CreateMap<LibroDto, VLibro>().ReverseMap();
            CreateMap<TipoAutorDto, Tipoautor>().ReverseMap();
            CreateMap<TipoLibroDto, Tipolibro>().ReverseMap();
            CreateMap<Librosautore, LibroAutorCreatedDto>().ReverseMap();
            CreateMap<Inventariolibro, InventarioDto>().ReverseMap();
            CreateMap<Usuario, UsuarioCreatedDto>().ReverseMap();
            CreateMap<Estadoconservacion, EstadoDto>().ReverseMap();
            CreateMap<Autenticidad, AutenticidadDto>().ReverseMap();



        }
    }
}

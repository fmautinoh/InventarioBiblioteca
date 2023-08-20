using AutoMapper;
using InventarioBiblioteca.Modelos.ModelDto;
using InventarioBiblioteca.Modelos;
using InventarioBiblioteca.Repositorio.IRepositorio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace InventarioBiblioteca.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutenticidadController : ControllerBase
    {
        private readonly IAutenticidadRepositorio _autenticidadRepositorio;
        private readonly IMapper _mapper;
        protected APIResponse _apiResponse;

        public AutenticidadController(IAutenticidadRepositorio autenticidadRepositorio, IMapper mapper)
        {
            _apiResponse = new APIResponse();
            _autenticidadRepositorio = autenticidadRepositorio;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("/ListaAutenticidad")]
        [ProducesResponseType(200)]//ok
        [ProducesResponseType(400)]//badreq
        [ProducesResponseType(404)]//no found
        public async Task<ActionResult<APIResponse>> GetTipoAutor()
        {
            try
            {
                IEnumerable<Autenticidad> autenticidad = await _autenticidadRepositorio.ListObjetos();
                _apiResponse.Alertmsg = "Listado Exitosamente";
                _apiResponse.Resultado = _mapper.Map<IEnumerable<AutenticidadDto>>(autenticidad);
                _apiResponse.StatusCode = HttpStatusCode.OK;
                return Ok(_apiResponse);
            }
            catch (Exception ex)
            {
                _apiResponse.IsSuccess = false;
                _apiResponse.ErrorMessage = new List<string> { ex.ToString() };
            }
            return _apiResponse;
        }
    }
}

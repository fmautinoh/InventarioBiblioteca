using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using InventarioBiblioteca.Modelos.ModelDto;
using InventarioBiblioteca.Repositorio.IRepositorio;
using InventarioBiblioteca.Modelos;

namespace InventarioBiblioteca.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TipoLibroController : ControllerBase
    {
        private readonly ITipoLibroRepositorio _tipolibrorrepo;
        private readonly IMapper _mapper;
        protected APIResponse _apiResponse;

        public TipoLibroController(ITipoLibroRepositorio tipolibroRepositorio, IMapper mapper)
        {
            _apiResponse = new APIResponse();
            _tipolibrorrepo = tipolibroRepositorio;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("/ListaTipoLibro")]
        [ProducesResponseType(200)]//ok
        [ProducesResponseType(400)]//badreq
        [ProducesResponseType(404)]//no found
        public async Task<ActionResult<APIResponse>> GetAutor()
        {
            try
            {
                IEnumerable<Tipolibro> tipoautorlist = await _tipolibrorrepo.ListObjetos();
                _apiResponse.Alertmsg = "Listado Exitosamente";
                _apiResponse.Resultado = _mapper.Map<IEnumerable<TipoLibroDto>>(tipoautorlist);
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

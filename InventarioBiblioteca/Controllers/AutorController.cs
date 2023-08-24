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
    public class AutorController : ControllerBase
    {
        private readonly IAutorRepositorio _autorrepo;
        private readonly IvautorRepositorio _vautorrepo;
        private readonly IMapper _mapper;
        protected APIResponse _apiResponse;

        public AutorController(IAutorRepositorio autorRepositorio, IMapper mapper, IvautorRepositorio vautorrepo)
        {
            _apiResponse = new APIResponse();
            _autorrepo = autorRepositorio;
            _vautorrepo = vautorrepo;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("/ListaAutores")]
        [ProducesResponseType(200)]//ok
        [ProducesResponseType(400)]//badreq
        [ProducesResponseType(404)]//no found
        public async Task<ActionResult<VAutor>> GetAutor()
        {
            try
            {
                var resultado = await _vautorrepo.ListObjetos();
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                _apiResponse.IsSuccess = false;
                _apiResponse.ErrorMessage = new List<string> { ex.ToString() };
            }
            return BadRequest(new { _apiResponse });
        }

        [HttpGet]
        [Route("/ListaAutor/{idaut:int}")]
        [ProducesResponseType(200)]//ok
        [ProducesResponseType(400)]//badreq
        [ProducesResponseType(404)]//no found
        public async Task<ActionResult<VAutor>> GetAutorporID(int idaut)
        {
            try
            {
                if (idaut == 0)
                {
                    _apiResponse.StatusCode = HttpStatusCode.BadRequest;
                    _apiResponse.IsSuccess = false;

                    return BadRequest(_apiResponse);
                }
                var autor = await _autorrepo.ListObjetos(c => c.Autorid == idaut);
                if (autor == null)
                {
                    _apiResponse.Alertmsg = "Autor no Encontrado";
                    _apiResponse.StatusCode = HttpStatusCode.NotFound;
                    _apiResponse.IsSuccess = false;

                    return NotFound(_apiResponse);
                }

                var resultado = await _vautorrepo.ListObjetos(x=> x.autorID == idaut);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                _apiResponse.IsSuccess = false;
                _apiResponse.ErrorMessage = new List<string> { ex.ToString() };
            }
            return BadRequest(new { _apiResponse });
        }

        [HttpPost]
        [Route("/CreateAutor")]
        [ProducesResponseType(200)]//ok
        [ProducesResponseType(400)]//badreq
        [ProducesResponseType(500)]//Internal Error
        [ProducesResponseType(404)]//no found
        [ProducesResponseType(204)]//No content
        [ProducesResponseType(409)]//no found


        public async Task<ActionResult<VAutor>> CrearAutor([FromBody] AutorCreatedDto ModelAutor)
        {
            try
            {
                if (ModelAutor == null)
                {
                    _apiResponse.StatusCode = HttpStatusCode.BadRequest;
                    _apiResponse.IsSuccess = false;

                    return BadRequest(_apiResponse);
                }

                var isexistente = await _autorrepo.ListObjetos(c => c.Nombreautor == ModelAutor.NombreAutor);
                if (isexistente.Count != 0)
                {
                    var message = "Autor Existente";
                    _apiResponse.StatusCode = HttpStatusCode.BadRequest;
                    _apiResponse.IsSuccess = false;
                    _apiResponse.Alertmsg = message;
                    return BadRequest(_apiResponse);
                }

                if (!ModelState.IsValid)
                {
                    var message = "Campos Invalidos";
                    _apiResponse.StatusCode = HttpStatusCode.BadRequest;
                    _apiResponse.IsSuccess = false;
                    _apiResponse.Resultado = ModelState;
                    _apiResponse.Alertmsg = message;
                    return BadRequest(_apiResponse);
                }

                Autore AutoreCrt = _mapper.Map<Autore>(ModelAutor);
                await _autorrepo.Crear(AutoreCrt);
                var resultado = await _vautorrepo.ObtenerPrimerElementoDescendente(ordenarPor: x => x.autorID);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                _apiResponse.IsSuccess = false;
                _apiResponse.ErrorMessage = new List<string> { ex.ToString() };
            }
            return BadRequest(new { _apiResponse });
        }

        [HttpPut]
        [Route("/updateAutor/{idaut:int}")]
        [ProducesResponseType(200)]//ok
        [ProducesResponseType(400)]//badreq
        [ProducesResponseType(500)]//Internal Error
        [ProducesResponseType(404)]//no found
        [ProducesResponseType(204)]//No content

        public async Task<ActionResult<VAutor>> UpdateLibro(int idaut, [FromBody] AutorCreatedDto ModelAutor)
        {
            try
            {
                if (idaut == 0)
                {
                    _apiResponse.StatusCode = HttpStatusCode.BadRequest;
                    _apiResponse.IsSuccess = false;
                    return BadRequest(_apiResponse);
                }

                Autore mdAutorUp = new()
                {
                    Autorid = idaut,
                    Nombreautor = ModelAutor.NombreAutor,
                    Tipoautorid = ModelAutor.TipoAutorId
                };
                await _autorrepo.Actualizar(mdAutorUp);
                var resultado = await _vautorrepo.Listar(x => x.autorID == idaut, tracked: false);
                return Ok(resultado);
            }
            catch (Exception e)
            {
                _apiResponse.IsSuccess = false;
                _apiResponse.ErrorMessage = new List<string> { e.ToString() };
            }
            return BadRequest(new { _apiResponse });
        }
    }
}

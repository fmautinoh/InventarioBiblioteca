using AutoMapper;
using InventarioBiblioteca.Modelos;
using InventarioBiblioteca.Modelos.ModelDto;
using InventarioBiblioteca.Repositorio.IRepositorio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace InventarioBiblioteca.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class InventarioController : ControllerBase
    {
        private readonly IvInventarioRepositorio _vInvenrepo;
        private readonly IInventarioRepositorio _Invrepo;
        private readonly IMapper _mapper;
        protected APIResponse _apiResponse;
        public InventarioController(IInventarioRepositorio inventarioRepositorio, IvInventarioRepositorio vistarepo, IMapper mapper)
        {
            _vInvenrepo = vistarepo;
            _apiResponse = new APIResponse();
            _mapper = mapper;
            _Invrepo = inventarioRepositorio;
        }

        [HttpGet]
        [Route("/ListaInventario")]
        [ProducesResponseType(200)]//ok
        [ProducesResponseType(400)]//badreq
        [ProducesResponseType(404)]//no found
        public async Task<ActionResult<APIResponse>> GetInventario()
        {
            try
            {
                IEnumerable<VInventario> Invlist = await _vInvenrepo.ListObjetos();
                return Ok(Invlist);
            }
            catch (Exception ex)
            {
                _apiResponse.IsSuccess = false;
                _apiResponse.ErrorMessage = new List<string> { ex.ToString() };
            }
            return _apiResponse;
        }

        [HttpGet]
        [Route("/ListaInventario/{idInv:int}")]
        [ProducesResponseType(200)]//ok
        [ProducesResponseType(400)]//badreq
        [ProducesResponseType(404)]//no found
        public async Task<ActionResult<APIResponse>> GetAutorporID(int idInv)
        {
            try
            {
                if (idInv == 0)
                {
                    _apiResponse.StatusCode = HttpStatusCode.BadRequest;
                    _apiResponse.IsSuccess = false;

                    return BadRequest(_apiResponse);
                }
                var inven = await _vInvenrepo.ListObjetos(c => c.Inventarioid == idInv);
                if (inven == null)
                {
                    _apiResponse.Alertmsg = "Libro no encontrado no Encontrado";
                    _apiResponse.StatusCode = HttpStatusCode.NotFound;
                    _apiResponse.IsSuccess = false;

                    return NotFound(_apiResponse);
                }
                return Ok(inven);
            }
            catch (Exception ex)
            {
                _apiResponse.IsSuccess = false;
                _apiResponse.ErrorMessage = new List<string> { ex.ToString() };
            }
            return _apiResponse;
        }

        [HttpPost]
        [Route("/CreateInventario")]
        [ProducesResponseType(200)]//ok
        [ProducesResponseType(400)]//badreq
        [ProducesResponseType(500)]//Internal Error
        [ProducesResponseType(404)]//no found
        [ProducesResponseType(204)]//No content
        [ProducesResponseType(409)]//no found

        public async Task<ActionResult<APIResponse>> CreateInventario([FromBody] InventarioDto ModelInv)
        {
            try
            {
                if (ModelInv == null)
                {
                    _apiResponse.StatusCode = HttpStatusCode.BadRequest;
                    _apiResponse.IsSuccess = false;

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

                Inventariolibro InventCrt = _mapper.Map<Inventariolibro>(ModelInv);
                await _Invrepo.Crear(InventCrt);
                return Ok(InventCrt);
            }
            catch (Exception ex)
            {
                _apiResponse.IsSuccess = false;
                _apiResponse.ErrorMessage = new List<string> { ex.ToString() };
            }
            return _apiResponse;
        }

        [HttpPut]
        [Route("/updateInventario/{idInv:int}")]
        [ProducesResponseType(200)]//ok
        [ProducesResponseType(400)]//badreq
        [ProducesResponseType(500)]//Internal Error
        [ProducesResponseType(404)]//no found
        [ProducesResponseType(204)]//No content

        public async Task<IActionResult> UpdateInventario(int idInv, [FromBody] InventarioDto ModelInv)
        {
            if (idInv == 0)
            {
                _apiResponse.StatusCode = HttpStatusCode.BadRequest;
                _apiResponse.IsSuccess = false;
                return BadRequest(_apiResponse);
            }
            Inventariolibro mdInvUp = new()
            {
                Inventarioid = idInv,
                Libroid = ModelInv.LibroId,
                Codigo = ModelInv.Codigo,
                Estadoid = ModelInv.EstadoId,
                Autenticidadid=ModelInv.Autenticidadid
            };

            await _Invrepo.Actualizar(mdInvUp);
            return Ok(mdInvUp);
        }

        [HttpDelete]
        [Route("/deleteLibros/{idlib:int}")]
        [ProducesResponseType(200)]//ok
        [ProducesResponseType(400)]//badreq
        [ProducesResponseType(500)]//Internal Error
        [ProducesResponseType(404)]//no found
        [ProducesResponseType(204)]//No content

        public async Task<IActionResult> DeleteLib(int idlib)
        {
            try
            {
                var MdLibro = await _Invrepo.Listar(c => c.Inventarioid == idlib, tracked: false);

                Inventariolibro deleteLB = new()
                {
                    Inventarioid = idlib,
                    Codigo = MdLibro.Codigo,
                    Estadoid = MdLibro.Estadoid,
                    Libroid = MdLibro.Libroid,
                    Autenticidadid = MdLibro.Autenticidadid
                };

                await _Invrepo.Remover(deleteLB);
                return Ok(deleteLB);
            }
            catch (Exception ex)
            {

                _apiResponse.IsSuccess = false;
                _apiResponse.ErrorMessage = new List<string> { ex.ToString() };
            }
            return Ok(_apiResponse);
        }


    }
}

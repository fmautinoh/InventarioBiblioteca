using InventarioBiblioteca.Modelos;
using InventarioBiblioteca.Modelos.ModelDto;
using InventarioBiblioteca.Repositorio.IRepositorio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Net;

namespace InventarioBiblioteca.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepositorio _usuariorepo;
        private APIResponse _apiResponse;
        public UsuarioController(IUsuarioRepositorio usuarioRepositorio)
        {
            _usuariorepo = usuarioRepositorio;
            _apiResponse = new();
        }

        [HttpPost]
        [Route("/Register")]
        [ProducesResponseType(200)]//ok
        [ProducesResponseType(400)]//badreq
        [ProducesResponseType(500)]//Internal Error
        [ProducesResponseType(404)]//no found
        [ProducesResponseType(204)]//No content
        [ProducesResponseType(409)]//no found
        public async Task<ActionResult<APIResponse>> Register([FromBody] UsuarioCreatedDto modelCrt)
        {
            if (!ModelState.IsValid)
            {
                var message = "Campos Invalidos";
                _apiResponse.StatusCode = HttpStatusCode.BadRequest;
                _apiResponse.IsSuccess = false;
                _apiResponse.Resultado = ModelState;
                _apiResponse.Alertmsg = message;
                return BadRequest(_apiResponse);
            }
                var reg = await _usuariorepo.Register(modelCrt);
            if(reg == null)
            {
                var message = "UserName ya existe";
                _apiResponse.StatusCode = HttpStatusCode.BadRequest;
                _apiResponse.IsSuccess = false;
                _apiResponse.Resultado = ModelState;
                _apiResponse.Alertmsg = message;
                return BadRequest(_apiResponse);
            }
            _apiResponse.Alertmsg = "Usuario Creado Exitosamente";
            _apiResponse.Resultado = modelCrt;
            _apiResponse.StatusCode = HttpStatusCode.Created;
            return Ok(_apiResponse);
        }

        [HttpPost]
        [Route("/Login")]
        public async Task<IActionResult> Login([FromBody] UsuarioDto model)
        {
            var loginresponse = await _usuariorepo.Login(model);
            if (loginresponse.Usuario == null || string.IsNullOrEmpty(loginresponse.Token))
            {
                _apiResponse.StatusCode = HttpStatusCode.BadRequest;
                _apiResponse.IsSuccess = false;
                _apiResponse.Resultado = loginresponse;
                _apiResponse.ErrorMessage.Add("UserName o Password son Incorrectos");
                _apiResponse.Alertmsg = "UserName o Password son Incorrectos";
                return BadRequest(_apiResponse);
            }
            _apiResponse.IsSuccess = true;
            _apiResponse.Alertmsg = "Login Success";
            _apiResponse.StatusCode = HttpStatusCode.OK;
            _apiResponse.Resultado = loginresponse;
            return Ok(_apiResponse);
        }
    }
}

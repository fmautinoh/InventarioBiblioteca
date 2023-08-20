﻿using AutoMapper;
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
    public class TipoAutorController : ControllerBase
    {
        private readonly ITipoAutorRepositorio _tipoautorrepo;
        private readonly IMapper _mapper;
        protected APIResponse _apiResponse;

        public TipoAutorController(ITipoAutorRepositorio tipoautorRepositorio, IMapper mapper)
        {
            _apiResponse = new APIResponse();
            _tipoautorrepo = tipoautorRepositorio;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("/ListaTipoAutores")]
        [ProducesResponseType(200)]//ok
        [ProducesResponseType(400)]//badreq
        [ProducesResponseType(404)]//no found
        public async Task<ActionResult<APIResponse>> GetTipoAutor()
        {
            try
            {
                IEnumerable<Tipoautor> tipoautorlist = await _tipoautorrepo.ListObjetos();
                _apiResponse.Alertmsg = "Listado Exitosamente";
                _apiResponse.Resultado = _mapper.Map<IEnumerable<TipoAutorDto>>(tipoautorlist);
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
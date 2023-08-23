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
    public class AutorController : ControllerBase
    {
        private readonly IAutorRepositorio _autorrepo;
        private readonly IMapper _mapper;
        protected APIResponse _apiResponse;

        public AutorController(IAutorRepositorio autorRepositorio, IMapper mapper)
        {
            _apiResponse = new APIResponse();
            _autorrepo = autorRepositorio;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("/ListaAutores")]
        [ProducesResponseType(200)]//ok
        [ProducesResponseType(400)]//badreq
        [ProducesResponseType(404)]//no found
        public async Task<ActionResult<APIResponse>> GetAutor()
        {
            try
            {
                IEnumerable<Autore> autorlist = await _autorrepo.ListObjetos();
                return Ok(_mapper.Map<IEnumerable<AutorDto>>(autorlist));
            }
            catch (Exception ex)
            {
                _apiResponse.IsSuccess = false;
                _apiResponse.ErrorMessage = new List<string> { ex.ToString() };
            }
            return _apiResponse;
        }

        [HttpGet]
        [Route("/ListaAutor/{idaut:int}")]
        [ProducesResponseType(200)]//ok
        [ProducesResponseType(400)]//badreq
        [ProducesResponseType(404)]//no found
        public async Task<ActionResult<APIResponse>> GetAutorporID(int idaut)
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
                return Ok(_mapper.Map<IEnumerable<AutorDto>>(autor));
            }
            catch (Exception ex)
            {
                _apiResponse.IsSuccess = false;
                _apiResponse.ErrorMessage = new List<string> { ex.ToString() };
            }
            return _apiResponse;
        }

        [HttpPost]
        [Route("/CreateAutor")]
        [ProducesResponseType(200)]//ok
        [ProducesResponseType(400)]//badreq
        [ProducesResponseType(500)]//Internal Error
        [ProducesResponseType(404)]//no found
        [ProducesResponseType(204)]//No content
        [ProducesResponseType(409)]//no found


        public async Task<ActionResult<APIResponse>> CrearAutor([FromBody] AutorCreatedDto ModelAutor)
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
                var resultado = await _autorrepo.Listar(c => c.Nombreautor == AutoreCrt.Nombreautor);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                _apiResponse.IsSuccess = false;
                _apiResponse.ErrorMessage = new List<string> { ex.ToString() };
            }
            return _apiResponse;
        }

        [HttpPut]
        [Route("/updateAutor/{idaut:int}")]
        [ProducesResponseType(200)]//ok
        [ProducesResponseType(400)]//badreq
        [ProducesResponseType(500)]//Internal Error
        [ProducesResponseType(404)]//no found
        [ProducesResponseType(204)]//No content

        public async Task<IActionResult> UpdateLibro(int idaut, [FromBody] AutorCreatedDto ModelAutor)
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
            return Ok(mdAutorUp);
        }



    }
}

using AutoMapper;
using C_50285_Nardelli_Nancy_Web_Api.DTOs;
using C_50285_Nardelli_Nancy_Web_Api.Models;
using C_50285_Nardelli_Nancy_Web_Api.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace C_50285_Nardelli_Nancy_Web_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        protected readonly ApiResponse _response;
        public ProductoController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _response = new();

        }
        [HttpPost("/api/Producto")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse>> Create([FromBody] ProductoDTO productoDTO)
        {
            try
            {
                if (!ModelState.IsValid) { return BadRequest(ModelState); }
                if (await _unitOfWork.ProductoRepositorio.Get(v => v.Descripciones.ToLower() == productoDTO.Descripciones.ToLower()) != null)
                {
                    ModelState.AddModelError("UsuarioExiste", "El usuario con ese nombre ya existe");
                    return BadRequest(ModelState);
                }

                if (productoDTO == null)
                {
                    return BadRequest(productoDTO);
                }
                Producto model = _mapper.Map<Producto>(productoDTO);

                await _unitOfWork.ProductoRepositorio.Create(model);
                _response.Result = model;
                _response.StatusCode = HttpStatusCode.Created;

                return CreatedAtRoute("getUser", new { id = productoDTO.Id }, _response);

            }
            catch (Exception ex)
            {

                _response.IsSucces = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };

            }
            return _response;

        }
    }
}

using AutoMapper;
using C_50285_Nardelli_Nancy_Web_Api.DTOs;
using C_50285_Nardelli_Nancy_Web_Api.Infrastructure;
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
                    ModelState.AddModelError("ProductoExiste", "El producto con esa descripción ya existe");
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

                return CreatedAtRoute("GetProduct", new { id = productoDTO.Id }, _response);

            }
            catch (Exception ex)
            {

                _response.IsSucces = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };

            }
            return _response;

        }

        [HttpGet("id:int", Name = "GetProduct")]
        [ProducesResponseType(typeof(ApiSuccessResponse<Producto>), 200)]
        public async Task<IActionResult> GetById(int id)
        {

            var producto = await _unitOfWork.UsuarioRepositorio.GetById(id);
            if (producto == null)
            {
                return NotFound(); // Devolver 404 si no se encuentra el usuario
            }
            var productoDto = _mapper.Map<ProductoDTO>(producto);
            return ResponseFactory.CreateSuccessResponse(200, productoDto);
        }

        [HttpPut("/api/Producto/{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, [FromBody] ProductoDTO updateDto)
        {
            if (updateDto == null || id != updateDto.Id)
            {
                _response.IsSucces = false;
                _response.StatusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }

            Producto modelo = _mapper.Map<Producto>(updateDto);

            await _unitOfWork.ProductoRepositorio.Update(modelo);
            _response.StatusCode = HttpStatusCode.NoContent;

            return Ok(_response);

        }

        [HttpDelete("/api/Producto/{id:int}")]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Delete(int id)
        {
            var producto = await _unitOfWork.ProductoRepositorio.GetById(id);
            if (producto == null)
            {
                _response.StatusCode = HttpStatusCode.NotFound;
                _response.IsSucces = false;
                _response.ErrorMessages.Add($"Producto con ID: {id} no encontrado.");
                return NotFound(_response);

            }

            await _unitOfWork.ProductoRepositorio.Delete(producto);
            // Configurar ApiResponse para indicar éxito
            _response.IsSucces = true;
            return NoContent();
        }
    }
}

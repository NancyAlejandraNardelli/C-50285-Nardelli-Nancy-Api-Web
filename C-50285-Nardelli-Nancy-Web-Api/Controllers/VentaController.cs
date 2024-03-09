using AutoMapper;
using C_50285_Nardelli_Nancy_Web_Api.DTOs;
using C_50285_Nardelli_Nancy_Web_Api.Infrastructure;
using C_50285_Nardelli_Nancy_Web_Api.Models;
using C_50285_Nardelli_Nancy_Web_Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace C_50285_Nardelli_Nancy_Web_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VentaController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        protected readonly ApiResponse _response;
        private readonly ILogger<ProductoVendidoController> _logger;
        public VentaController(ILogger<ProductoVendidoController> logger, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _response = new();
            _logger = logger;
        }

        [HttpGet("{idUsuario:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<VentaDTO>>> GetVentasByUserId(int idUsuario)
        {
            var ventas = await _unitOfWork.VentaRepositorio.GetVentasByUserId(idUsuario);

            if (ventas == null || ventas.Count == 0)
            {
                return NotFound();
            }

            var ventasDTO = _mapper.Map<List<VentaDTO>>(ventas);
            return Ok(ventasDTO);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse>> Create([FromBody] VentaDTO ventaDTO)
        {
            try
            {
                if (!ModelState.IsValid) { return BadRequest(ModelState); }
                

                if (ventaDTO == null)
                {
                    return BadRequest(ventaDTO);
                }
                Ventum model = _mapper.Map<Ventum>(ventaDTO);

                await _unitOfWork.VentaRepositorio.Create(model);
                _response.Result = model;
                _response.StatusCode = HttpStatusCode.Created;

                return CreatedAtRoute("GetVenta", new { id = ventaDTO.Id }, _response);

            }
            catch (Exception ex)
            {

                _response.IsSucces = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };

            }
            return _response;

        }

        [HttpGet("id:int", Name = "GetVenta")]
        [ProducesResponseType(typeof(ApiSuccessResponse<Ventum>), 200)]
        public async Task<IActionResult> GetById(int id)
        {

            var venta = await _unitOfWork.VentaRepositorio.GetById(id);
            if (venta == null)
            {
                return NotFound(); // Devolver 404 si no se encuentra el usuario
            }
            var VentaDTO = _mapper.Map<VentaDTO>(venta);
            return ResponseFactory.CreateSuccessResponse(200, VentaDTO);
        }
    }
}
